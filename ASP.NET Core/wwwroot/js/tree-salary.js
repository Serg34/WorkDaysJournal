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
function SetDisplayToInnerRows(mainRow, value) {
    var rows = document.getElementsByClassName(mainRow.id);
    for (var i = 0; i < rows.length; i++) {
        rows[i].style.display = value;
    }
}

function SelectRow(row, tableId) {

    var table = document.getElementById(tableId);
    for (var i = 1; i < table.rows.length; i++) {
        table.rows[i].style.backgroundColor = "";
    }
    var bg = window.getComputedStyle(row).backgroundColor;
    if (bg === "rgba(0, 0, 0, 0)") {
        row.style.backgroundColor = "rgb(235,235,235)";
    } else {
        bg = bg.replace("rgb(", "").replace(")", "");
        var items = bg.split(",");
        var r = Number(items[0]);
        var g = Number(items[1]);
        var b = Number(items[2]);
        row.style.backgroundColor = "rgb(" + (r - 20) + ", " + (g - 20) + ", " + (b - 20) + ")";
    }
}
