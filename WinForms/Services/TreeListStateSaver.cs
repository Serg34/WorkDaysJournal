using Furmanov.Dal;
using Furmanov.Data.Data;
using Furmanov.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;
using System.Runtime.Serialization;

namespace Furmanov.UI.Services
{
	public sealed class TreeListStateSaver : IDisposable
	{
		private readonly TreeListView _treeList;
		private List<string> _expandedIds = new List<string>();
		private string _selectedId;
		private string _selectedColumnName;
		private int _topItemIndex;
		private readonly object _tag;

		public TreeListStateSaver(TreeListView treeList)
		{
			_treeList = treeList;
			_treeList.BeginUpdate();
			_treeList.ExpandedObjects.ForEach(GetExpanded);
			_selectedId = _treeList.SelectedObject is IViewModel vm ? vm.ViewModelId : null;
			_topItemIndex = _treeList.TopItemIndex;
			_tag = _treeList.Tag;
			_treeList.Tag = Updating;
		}
		public static object Updating { get; } = new object();

		private void GetExpanded(object node)
		{
			if (node is IViewModel vm)
			{
				_expandedIds.Add(vm.ViewModelId);
			}
		}
		private void SetState(object node)
		{
			if (!(node is IViewModel vm)) return;
			if (_expandedIds?.Any(id => vm.ViewModelId == id) ?? false) _treeList.Expand(node);
			if (vm.ViewModelId == _selectedId && _selectedId.NoEmpty())
			{
				_treeList.SelectedObject = node;
			}

			var childrens = _treeList.GetChildren(node);
			childrens.ForEach(SetState);
		}

		public void SaveLayoutToXml(string xmlFile)
		{
			new XmlRepository<Data>(xmlFile).Save(new Data
			{
				ExpandedIds = _expandedIds,
				SelectedColumnName = _selectedColumnName,
				SelectedId = _selectedId,
				TopVisibleNodeIndex = _topItemIndex,
			});
		}
		public void RestoreLayoutFromXml(string xmlFile)
		{
			var data = new XmlRepository<Data>(xmlFile).Load();
			_expandedIds = data.ExpandedIds;
			_selectedColumnName = data.SelectedColumnName;
			_selectedId = data.SelectedId;
			_topItemIndex = data.TopVisibleNodeIndex;
		}

		public void Dispose()
		{
			try
			{
				_treeList.Objects.ForEach(SetState);
				_treeList.TopItemIndex = _topItemIndex;
				_treeList.Tag = _tag;

				_treeList.EndUpdate();
			}
			catch { }
		}

		[DataContract]
		[Serializable]
		public class Data
		{
			[DataMember]
			public List<string> ExpandedIds { get; set; }
			[DataMember]
			public string SelectedId { get; set; }
			[DataMember]
			public string SelectedColumnName { get; set; }
			[DataMember]
			public int TopVisibleNodeIndex { get; set; }
		}
	}
}