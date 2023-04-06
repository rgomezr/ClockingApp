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

function DeleteClocking(clockingId) {
    Swal.fire({
        icon: 'question',
        title: 'Removing Clocking',
        text: `This will remove Clocking ${clockingId}`,
        showConfirmButton: true,
        showCancelButton: true,
        confirmButtonText: 'Delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            //AJAX to send cancel request to server
            // TODO: Modularise this method below
            $.ajax({
                type: "DELETE",
                url: "Clocking/Clocking",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(clockingId),
                success: function (response) {
                    if (response === true) {
                        Swal.fire('Deleted!'
                            , 'Clocking is gone'
                            , 'success');
                    } else {
                        Swal.fire('Error!'
                            , 'Something went wrong'
                            , 'error');
                    }
                } 
            });
        }
        //else if (result.dismiss === Swal.DismissReason.cancel) {
        //    console.log('cancel');
        //}

    });

}