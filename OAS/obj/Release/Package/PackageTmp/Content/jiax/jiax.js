function initQuery() {
    $("#btnQuery").click(function () {
        var jiaxname = $("#jiaxname").val();
        var areano = "";
        try {
            areano = $("#ddlareano").combobox("getValue");
        } catch (e) {

        }
        $("#list").datagrid({
            url: '/Jiax/Index',
            queryParams: { jiaxname: jiaxname, areano: areano, rnd: Math.random() },
            cls: 'table-wrap',
            fit: true,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            columns: [[
                { field: 'jiaxid', title: '驾校ID' },
                { field: 'jiaxname', title: '驾校名称' },
                { field: 'areaname', title: '区域名称' },
                { field: 'jiaxcontacter', title: '联系人' },
                { field: 'jiaxtel', title: '固定电话' },
                { field: 'jiaxmobile', title: '手机' },
                { field: 'jiaxaddress', title: '地址' }
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
        showDialog("editframe", "/Jiax/Edit?a=1" + Math.random(), {
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
                $('#areano').combobox({
                    onSelect: function (record) {
                        $('#areaname').val(record.text);
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
            var jiaxid = row.jiaxid;
            showDialog("editframe", "/Jiax/Edit?a=1" + Math.random() + "&jiaxid=" + jiaxid, {
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
                    $('#areano').combobox({
                        onSelect: function (record) {
                            $('#areaname').val(record.text);
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
                    $.post("/jiax/delete", { jiaxid: row.jiaxid, rnd: Math.random() }, function (data) {
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
        if ($("#editform").attr("action").toLowerCase() == "/jiax/add") {
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