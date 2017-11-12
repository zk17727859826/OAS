$("#btnLogin").click(function () {
    var userno = $("#userno").val();
    var userpwd = $("#userpwd").val();

    var _this = this;
    $(_this).prop("disabled", true).text("登陆中...");
    $.post("/login/index", { userno: userno, password: userpwd, rnd: Math.random() }, function (data) {
        if (data && data.success === true) {
            $(_this).text("页面跳转中...");
            window.location = "/Home/Index";
        }
        else {
            if (data) {
                $.messager.alert("警告", decodeURIComponent(data.message), "info");
            }
            else {
                $.messager.alert("警告", "登陆失败", "info");
            }
            $(_this).prop("disabled", false).text("登 陆");
        }
    }, "json").error(function () {
        $.messager.alert("警告", "登陆失败", "info");
    });
});