function ShowMessage(message, isQuestion = false, func = null) {
    var messageBody = document.getElementById("messageModalBody");
    messageBody.innerHTML = message;

    var btMessageOk = document.getElementById("btMessageOk");
    var btMessageYes = document.getElementById("btMessageYes");
    var btMessageCancel = document.getElementById("btMessageCancel");

    $("#messageModal").modal("show");

    btMessageOk.style.display = isQuestion ? "none" : "block";
    btMessageYes.style.display =
    btMessageCancel.style.display = isQuestion ? "block" : "none";

    btMessageYes.onclick = func;
}

function ShowQuestion(question, func) {
    return ShowMessage(question, true, func);
}

function ReportBug(path, status, errorMsg) {
    var url = "/Exception/ReportBug?path=" + path + "&status=" + status + "&errorMsg=" + errorMsg;
    document.location.href = url;
}

function ShowNoImplementedCode() {
    ShowMessage("В реальном приложении нам этой кнопке не очень интересный код.<br/><br/>" +
        "Для демонстрации доступны следующие функции:<br/>" +
        "-Заполнение базы данных новыми записями;<br/>" +
        "-Тест отчёта о баге (желательно при запущенном приложении 'Bugs');<br/>" +
        "-Изменение месяца в пределах 2019г;<br/>" +
        "-Изменение дней выхода сотрудника на работу");
}
