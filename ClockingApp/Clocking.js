function StartWork(user) {
    $.ajax({
        type: "POST",
        url: "/Clocking/StartWork",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ username: user }),
        success: function (_dataBack) {
            console.log(_dataBack);
            window.location.href = _dataBack;
        }
    });
}

function StartBreak(clockingId) {
    $.ajax({
        type: "POST",
        url: "/Clocking/StartBreak",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ clockingId: clockingId }),
        success: function (_dataBack) {
            console.log(_dataBack);
            window.location.href = _dataBack;
        }
    });
}