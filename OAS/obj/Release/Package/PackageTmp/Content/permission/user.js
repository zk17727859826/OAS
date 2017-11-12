function initQuery() {
    $("#btnQuery").click(function () {
        var username = $("#username").val();
        var userno = $("#userno").val();
        $("#list").datagrid({
            url: '/User/Index',
            queryParams: { userno: userno, username: username, rnd: Math.random() },
            cls: 'table-wrap',
            fit: true,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            columns: [[
                { field: 'userno', title: '编号', width: 100 },
                { field: 'username', title: '姓名', width: 100 },
                { field: 'xingbie', title: '性别', width: 100 },
                { field: 'shenfhm', title: '身份号码', width: 100 },
                { field: 'shoujhm', title: '手机号码', width: 100 },
                { field: 'isvalid', title: '是否有效', width: 100 },
                //{ field: 'creater', title: '创建人', width: 100 },
                //{ field: 'createdate', title: '创建时间', width: 120, formatter: formatDate },
                { field: 'editer', title: '编辑人', width: 100 },
                { field: 'editdate', title: '编辑时间', width: 120, formatter: formatDate }
            ]]
        });
    }).trigger("click");
}

function initRefresh() {
    $("#btnRefresh").click(function () {
        $("#btnQuery").trigger("click");
    });
}

function initShowAdd() {
    $("#btnShowAdd").click(function () {
        showDialog("editframe", "/User/Edit?a=" + Math.random(), {
            buttons:[{
                text: '保存',
                iconCls: 'icon-ok',
                handler: function () {
                    $("#editform").submit();
                }
            },{
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    hideDialog("editframe");
                }
            }]
        });
    });
}

function initShowUpdate() {
    $("#btnShowEdit").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var userno = row.userno;
            showDialog("editframe", "/User/Edit?a=" + Math.random() + "&userno=" + userno, {
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        $("#editform").submit();
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hideDialog("editframe");
                    }
                }]
            });
        }
        else {
            $.messager.alert('警告', '请先选择一个用户');
        }
    });
}

function initDelete() {
    $("#btnDelete").click(function () {
        $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
            if (r) {
                var row = $("#list").datagrid("getSelected");
                if (row) {
                    $.post("/user/delete", { userno: row.userno, rnd: Math.random() }, function (data) {
                        if (data && data.success === true) {
                            var index = $("#list").datagrid("getRowIndex", $("#list").datagrid("getSelected"));
                            $("#list").datagrid("deleteRow", index);
                        }
                        else {
                            if (data) {
                                $.messager.alert("警告", decodeURIComponent(data.message));
                            }
                            else {
                                $.messager.alert("警告", "删除失败");
                            }
                        }
                    }, "json")
                }
                else {
                    $.messager.alert('警告', '请先选择一个用户');
                }
            }
        });
    });
}

function initShowPassword() {
    $("#btnEditPassword").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var userno = row.userno;
            showDialog("passwordframe", "/User/Password?a=" + Math.random() + "&userno=" + userno, {
                height: 200,
                buttons: [{
                    text: '保存',
                    iconCls: 'icon-ok',
                    handler: function () {
                        $("#passwordform").submit();
                    }
                }, {
                    text: '关闭',
                    iconCls: 'icon-cancel',
                    handler: function () {
                        hideDialog("passwordframe");
                    }
                }]
            });
        }
        else {
            $.messager.alert('警告', '请先选择一个用户');
        }
    });
}

function showLayout() {
    $.easyui.loading({ msg: "正在加载...", locale: "#editframe" });
}

function hideLayout() {
    $.easyui.loaded("#editframe");
}

function showUpdateResult(data) {
    if (data && data.success === true) {
        if ($("#editform").attr("action").toLowerCase() == "/user/add") {
            $("#list").datagrid('appendRow', data.rows);
        }
        else {
            $("#list").datagrid('updateRow', {
                index: $("#list").datagrid("getRowIndex", $("#list").datagrid("getSelected")),
                row: data.rows
            });
        }
        hideDialog("editframe");
        $.messager.alert("提示", "提交成功");        
    }
    else {
        if (data) {
            $.messager.alert("警告", decodeURIComponent(data.message));
        }
        else {
            $.messager.alert("警告", "提交失败");
        }
    }
}

function showUpdatePassword(data) {
    if (data && data.success === true) {
        hideDialog("passwordframe");
        $.messager.alert("提示", "提交成功");
    }
    else {
        if (data) {
            $.messager.alert("警告", decodeURIComponent(data.message));
        }
        else {
            $.messager.alert("警告", "提交失败");
        }
    }
}

function showLayoutForPwd() {
    try {
        var oldp = $("#repeatpassword").val();
        var oldn = $("#newpassword").val();
        if (oldp == "" || oldn == "") {
            throw "密码不能为空";
        }

        if (oldp != oldn) {
            throw "两次密码输入不一致";
        }

        $.easyui.loading({ msg: "正在加载...", locale: "#passwordframe" });
    } catch (e) {
        $.messager.alert("警告", e);
        return false;
    }
}

function hideLayoutForPwd() {
    $.easyui.loaded("#passwordframe");
}