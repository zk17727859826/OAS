function initQuery() {
    $("#btnQuery").click(function () {
        var serialno = $("#serialno").val();
        var serialname = $("#serialname").val();
        $("#list").datagrid({
            url: '/Serial/Index',
            queryParams: { serialno: serialno, serialname: serialname, rnd: Math.random() },
            cls: 'table-wrap',
            fit: true,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            columns: [[
                { field: 'serialno', title: '编码代码' },
                { field: 'serialname', title: '编号名称' },
                { field: 'prefix', title: '前缀' },
                { field: 'preflag', title: '前缀连接符' },
                { field: 'midfix', title: '中缀' },
                { field: 'midflag', title: '中缀连接符' },
                { field: 'lastfix', title: '后缀' },
                { field: 'lastflag', title: '后缀连接符' },
                { field: 'yearnum', title: '年份个数' },
                { field: 'monthnum', title: '显示月份' },
                { field: 'daynum', title: '显示年份' },
                { field: 'serialnum', title: '序列个数' },
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
        showDialog("editframe", "/Serial/Edit?a=1" + Math.random(), {
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
    });
}

function initShowUpdate() {
    $("#btnShowEdit").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var serialno = row.serialno;
            showDialog("editframe", "/Serial/Edit?a=1" + Math.random() + "&serialno=" + serialno, {
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
                    $.post("/serial/delete", { serialno: row.serialno, rnd: Math.random() }, function (data) {
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
        if ($("#editform").attr("action").toLowerCase() == "/serial/add") {
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

function initTest() {
    $("#btnTest").click(function () {
        var row = $("#list").datagrid("getSelected");
        if (row) {
            $.post("/Serial/Test", { serialno: row.serialno, specialword: "A", rdn: Math.random() }, function (rsp) {
                if (rsp && rsp.success === true) {
                    $.messager.alert("警告", rsp.message);
                }
                else {
                    if (rsp) {
                        $.messager.alert("警告", decodeURIComponent(rsp.message));
                    }
                    else {
                        $.messager.alert("测试失败");
                    }
                }
            }, "json");
        }
        else {
            $.messager.alert("警告", "请选择一条记录");
        }
    });
}