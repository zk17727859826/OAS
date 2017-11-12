function initQuery() {
    $("#btnQuery").click(function () {
        var jiaolh = $("#jiaolh").val();
        var xingming = $("#xingming").val();
        var shenfhm = $("#shenfhm").val();
        $("#list").datagrid({
            url: '/Jiaolian/Index',
            queryParams: { jiaolh: jiaolh, xingming: xingming, shenfhm: shenfhm, rnd: Math.random() },
            cls: 'table-wrap',
            fit: true,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            columns: [[
                { field: 'jiaolh', title: '教练号' },
                { field: 'jiaxname', title: '驾校' },
                { field: 'xingming', title: '姓名' },
                { field: 'xingbie', title: '性别' },
                { field: 'shenfhm', title: '身份号码' },
                { field: 'tel', title: '固定电话' },
                { field: 'mobile', title: '手机' },
                { field: 'email', title: '邮箱' },
                { field: 'address', title: '家庭地址' }
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
        showDialog("editframe", "/Jiaolian/Edit?a=1" + Math.random(), {
            height: 330,
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
            }],
            onLoad: function () {
                $('#sltJiaxid').combobox({
                    onSelect: function (record) {
                        $('#jiaxname').val(record.text);
                    }
                });
            }
        });
    });
}

function initShowUpdate() {
    $("#btnShowEdit").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var jiaolh = row.jiaolh;
            showDialog("editframe", "/Jiaolian/Edit?a=1" + Math.random() + "&jiaolh=" + jiaolh, {
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
                }],
                onLoad: function () {
                    $('#sltJiaxid').combobox({
                        onSelect: function (record) {
                            $('#jiaxname').val(record.text);
                        }
                    });
                }
            });
        }
        else {
            $.messager.alert('警告', '请先选择一条记录');
        }
    });
}

function initDelete() {
    $("#btnDelete").click(function () {
        $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
            if (r) {
                var row = $("#list").datagrid("getSelected");
                if (row) {
                    $.post("/Jiaolian/delete", { jiaolh: row.jiaolh, rnd: Math.random() }, function (data) {
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
                    $.messager.alert('警告', '请先选择一条记录');
                }
            }
        });
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
        if ($("#editform").attr("action").toLowerCase() == "/jiaolian/add") {
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