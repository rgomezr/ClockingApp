function StartWork() {
    $.ajax({
        type: "POST",
        url: "/Clocking/StartWork",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (_dataBack) {
            window.location.href = _dataBack;
        }
    });
}

function FinishWork(clockingId) {
    $.ajax({
        type: "POST",
        url: "/Clocking/FinishWork",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(clockingId),
        success: function (_dataBack) {
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
        data: JSON.stringify(clockingId),
        success: function (_dataBack) {
            window.location.href = _dataBack;
        }
    });
}
function FinishBreak(clockingId) {
    $.ajax({
        type: "POST",
        url: "/Clocking/FinishBreak",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(clockingId),
        success: function (_dataBack) {
            window.location.href = _dataBack;
        }
    });
}

//Events TODO: Separate events into another file

//function showOptionsForElement(event) {
//    var index = event.currentTarget.id;
//    var optionsElementForIndex = document.getElementById('optionsFor'+index);
//    optionsElementForIndex.classList.remove('invisible');
//}

//function hideOptionsForElement(event) {
//    var index = event.currentTarget.id;
//    var optionsElementForIndex = document.getElementById('optionsFor'+index);
//    optionsElementForIndex.classList.add('invisible');
//}


//$('.clockingTable tbody tr').mouseenter(function (event) {
//    showOptionsForElement(event);
//}).mouseleave(function (event) {
//    hideOptionsForElement(event);
//});