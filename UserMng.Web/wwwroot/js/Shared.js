


window.confirm = function (_text, _title, _yesAction = function () { }) {
    swal.fire({
        title: _title,
        text: _text,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#3085d6',
        confirmButtonColor: '#d33',
        confirmButtonText: 'باشه',
        cancelButtonText: 'نه'
    }).then((result) => {
        if (result.isConfirmed) {
            _yesAction();
        }
    });
}

function SendData(_url, _data, _Funcs_Success = function (res) { }) {

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _data,
        timeout: 600000,
        beforeSend: function (xhr) {
            /* $('.loading').show();*/

            //var securityToken = $("[name=__RequestVerificationToken]").val();
            //xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
            /*   $('.loading').hide(3000);*/
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function SendForm(_url, _FormId, _Funcs_Success = function (res) { }) {
    var form = $('#' + _FormId)[0];
    var _formdata = new FormData(form);

    $.ajax({
        type: "post",
        enctype: 'multipart/form-data',
        url: _url,
        data: _formdata,
        processData: false,
        contentType: false,
        cache: false,
        timeout: 600000,
        beforeSend: function (xhr) {

            $('.loading').show();

            var securityToken = $("[name=__RequestVerificationToken]").val();
            xhr.setRequestHeader("XSRF-TOKEN", securityToken);
        },
        success: function (response) {
            _Funcs_Success(response);
        },
        complete: function (data) {
            $('.loading').hide(100);
        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    });
}

function LoadComponenet(_Url, _Data, _CallbackFuncs = function (data) { }) {
    $.ajax({
        url: _Url,
        type: 'get',
        data: _Data,
        beforeSend: function (xhr) {
            $('.loading').show();

        },
        complete: function (data) {
            $('.loading').hide(50);

        },
        error: function (data) {
            if (data.status == 429) {
                Alert429();
            }
        }
    }).done(function (data) {
        _CallbackFuncs(data);
    });
}

function RemoveData(_Url, _Data = {}) {
    confirm('آیا از حذف این مورد مطمئن هستید؟ ', 'پیام', function () {
        SendData(_Url, _Data);
    });
}

function StatusData(_Url, _Data = {}) {
    confirm('آیا از تغییر وضعیت این مورد مطمئن هستید؟ ', 'پیام', function () {
        SendData(_Url, _Data);
    });
}