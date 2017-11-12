function initQuery() {
    $("#list").datagrid({
        url: '/PaperRule/Query',
        queryParams: { rnd: Math.random() },
        fit: true,
        rownumbers: true,
        border: false,
        singleSelect: true,
        autoRowHeight: false,
        pagination: true,
        onSelect: function (index, row) {
        },
        columns: [[
            //{ field: 'chexing', title: '车型' },
            { field: 'kemu', title: '科目', width: 100, formatter: showKemu },
            { field: 'single', title: '单选题比例' },
            { field: 'judge', title: '判断题比例' },
            { field: 'multi', title: '多选题比例' }
        ]]
    });
}

function initRefresh() {
    $("#btnRefresh").click(function () {
        initQuery();
    });
}

function initShowAdd() {
    $("#btnShowAdd").click(function () {
        showDialog("editframe", "/PaperRule/Edit?a=" + Math.random(), {
            height: 300,
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
            var pkid = row.pkid;
            showDialog("editframe", "/PaperRule/Edit?a=" + Math.random() + "&pkid=" + pkid, {
                height: 300,
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
            $.messager.alert('警告', '请先选择一行记录');
        }
    });
}

function initDelete() {
    $("#btnDelete").click(function () {
        $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
            if (r) {
                var row = $("#list").datagrid("getSelected");
                if (row) {
                    $.post("/PaperRule/delete", { pkid: row.pkid, rnd: Math.random() }, function (data) {
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
                    $.messager.alert('警告', '请先选择一行记录');
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
        if ($("#editform").attr("action").toLowerCase() == "/paperrule/add") {
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

function showKemu(value, row, index) {
    switch (value) {
        case "KM1":
            return "科目一"
        case "KM4":
            return "科目四"
        case "KMA":
            return "客车"
        case "KMB":
            return "货车"
        default:
            return "";
    }
}