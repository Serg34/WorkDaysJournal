﻿using DevExpress.Utils.Extensions;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Furmanov.Dal;
using Furmanov.Dal.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Furmanov.UI.Services
{
	public sealed class TreeListStateSaver : IDisposable
	{
		private readonly TreeList _treeList;
		private List<string> _expandedIds = new List<string>();
		private string _selectedId;
		private string _selectedColumnName;
		private int _topVisibleNodeIndex;

		public TreeListStateSaver(TreeList treeList)
		{
			_treeList = treeList;
			_treeList.Nodes.ForEach(GetExpanded);
			_selectedId = (_treeList.GetRow(_treeList.FocusedNode?.Id ?? -1) as IVisual)?.VisualId;
			_selectedColumnName = _treeList.FocusedColumn?.Name;
			_topVisibleNodeIndex = _treeList.TopVisibleNodeIndex;
		}

		private void GetExpanded(TreeListNode node)
		{
			if (node.Expanded && _treeList.GetRow(node.Id) is IVisual vm)
			{
				_expandedIds.Add(vm.VisualId);
			}
			node.Nodes?.ForEach(GetExpanded);
		}
		private void SetState(TreeListNode node)
		{
			if (!(_treeList.GetRow(node.Id) is IVisual vm)) return;
			if (_expandedIds?.Any(id => vm.VisualId == id) ?? false) node.Expanded = true;
			if (vm.VisualId == _selectedId)
			{
				_treeList.SetFocusedNode(node);
			}
			node.Nodes?.ForEach(SetState);
		}

		public void SaveLayoutToXml(string xmlFile)
		{
			new XmlRepository<Data>(xmlFile).Save(new Data
			{
				ExpandedIds = _expandedIds,
				SelectedColumnName = _selectedColumnName,
				SelectedId = _selectedId,
				TopVisibleNodeIndex = _topVisibleNodeIndex,
			});
		}
		public void RestoreLayoutFromXml(string xmlFile)
		{
			var data = new XmlRepository<Data>(xmlFile).Load();
			_expandedIds = data.ExpandedIds;
			_selectedColumnName = data.SelectedColumnName;
			_selectedId = data.SelectedId;
			_topVisibleNodeIndex = data.TopVisibleNodeIndex;
		}

		public void Dispose()
		{
			_treeList.BeginInit();

			_treeList.Nodes.ForEach(SetState);
			_treeList.TopVisibleNodeIndex = _topVisibleNodeIndex;

			_treeList.EndInit();

			var focusedColumn = _treeList.Columns
				.FirstOrDefault(c => c.Name == _selectedColumnName);
			if (focusedColumn != null) _treeList.FocusedColumn = focusedColumn;
		}

		[DataContract]
		[Serializable]
		private class Data
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
