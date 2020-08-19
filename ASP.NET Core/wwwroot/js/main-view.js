function ShowHideRow(id) {

    var expandRow = document.getElementById(id);
    var icon = document.getElementById(id + "_icon");
    var rows = document.getElementsByClassName(id);

    if (expandRow.getAttribute("data-expand") === "false") {
        expandRow.setAttribute("data-expand", "true");
        icon.innerHTML = "<i class=\"fa fa-caret-down\" aria-hidden=\"true\"></i>";
        for (var i = 0; i < rows.length; i++) {
            rows[i].style.display = "table-row";
            var display = rows[i].getAttribute("data-expand") === "true" ? "table-row" : "none";
            SetDisplayToInnerRows(rows[i], display);
        }
    } else {
        expandRow.setAttribute("data-expand", "false");
        icon.innerHTML = "<i class=\"fa fa-caret-right\" aria-hidden=\"true\"></i>";
        for (var j = 0; j < rows.length; j++) {
            rows[j].style.display = "none";
            SetDisplayToInnerRows(rows[j], "none");
        }
    }
}
function SetDisplayToInnerRows(mainRow, display) {
    var rows = document.getElementsByClassName(mainRow.id);
    for (var i = 0; i < rows.length; i++) {
        rows[i].style.display = display;
    }
}

function GetExpandList(tableId) {
    var expandList = new Array();
    var table = document.getElementById(tableId);
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].getAttribute("data-expand") === "true") {
            expandList[expandList.length] = table.rows[i].id;
        }
    }
    return expandList;
}

function SelectRow(rowId, tableId) {
    var selectRow = document.getElementById(rowId);
    if (selectRow === undefined || selectRow === null) return;
    if (selectRow.classList.contains("selected")) return; //если строка уже выделена

    var table = document.getElementById(tableId);
    for (var i = 0; i < table.rows.length; i++) {
        table.rows[i].style.backgroundColor = "";
        table.rows[i].classList.remove("selected");
    }
    var bg = window.getComputedStyle(selectRow).backgroundColor;
    if (bg === "rgba(0, 0, 0, 0)") {
        selectRow.style.backgroundColor = "rgb(240, 240, 240)";
    } else {
        bg = bg.replace("rgb(", "").replace(")", "");
        var items = bg.split(",");
        var r = Number(items[0]);
        var g = Number(items[1]);
        var b = Number(items[2]);
        selectRow.style.backgroundColor = "rgb(" + (r - 20) + ", " + (g - 20) + ", " + (b - 20) + ")";
    }
    selectRow.classList.add("selected");
}

function GetSelectRow(tableId) {
    var table = document.getElementById(tableId);
    for (var i = 0; i < table.rows.length; i++) {
        if (table.rows[i].classList.contains("selected")) {
            return table.rows[i].id;
        }
    }
    return "";
}

function GetWorkedDays(payId) {

    var url = "/Home/_WorkedDays?payId=" + payId;
    $.ajax({
        url: url,
        method: "get",
        dataType: "html",
        success: function (data) {
            var tableWorkDays = document.getElementById("table-days");
            tableWorkDays.innerHTML = data;
        }
    });
}

function SaveWorkedDays(payId, allDays, isExist) {

    var expandList = GetExpandList("tree-salary");
    var selectedRow = GetSelectRow("tree-salary");

    var model = {
        PayId: payId,
        AllDays: allDays,
        IsExist: isExist,
        ExpandList: expandList,
        SelectedRow: selectedRow
    };

    var json = JSON.stringify(model);

    var url = "/Home/SaveWorkedDays";
    $.ajax({
        url: url,
        method: "post",
        dataType: "html",
        data: { json: json },
        success: function (data) {
            html.innerHTML = data;
        }
    });
}

function SaveWorkedDay(payId, date, checkBox) {

    var expandList = GetExpandList("tree-salary");
    var selectedRow = GetSelectRow("tree-salary");

    var workedDay = {
        SalaryPay_Id: payId,
        DateJson: date,
        IsWorked: checkBox.checked
    };

    var model = {
        WorkedDay: workedDay,
        ExpandList: expandList,
        SelectedRow: selectedRow
    };

    var json = JSON.stringify(model);

    var url = "/Home/SaveWorkedDay";
    $.ajax({
        url: url,
        method: "post",
        dataType: "html",
        data: { json: json },
        success: function (data) {
            var treeSalary = document.getElementById("tree-salary");
            treeSalary.innerHTML = data;
        }
    });
}