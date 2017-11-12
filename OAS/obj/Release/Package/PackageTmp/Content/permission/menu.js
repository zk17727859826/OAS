function initQuery() {
    $("#list").treegrid({
        url: '/Module/Query',
        queryParams: { rnd: Math.random() },
        cls: 'table-wrap',
        fit: true,
        rownumbers: true,
        singleSelect: true,
        autoRowHeight: false,
        pagination: false,
        idField: 'menuno',
        treeField: 'menuname',
        columns: [[
            { field: 'menuname', title: '菜单名称', width: 200 },
            { field: 'menuno', title: '菜单编号', width: 200 },
            { field: 'isvalid', title: '是否有效', width: 100 },
            { field: 'menuurl', title: '菜单地址' },
            { field: 'menuclass', title: '菜单样式' },
            { field: 'menusort', title: '菜单排序' },
            { field: 'editer', title: '编辑人', width: 100 },
            { field: 'editdate', title: '编辑时间', width: 120, formatter: formatDate }
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
        var row = $("#list").datagrid("getSelected");
        var menuno = row == null ? "" : ("&menuno=" + row.menuno);
        showDialog("editframe", "/Module/Edit?type=add" + menuno + "&a=" + Math.random(), {
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
            }]
        });
    });
}

function initShowUpdate() {
    $("#btnShowEdit").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var menuno = row.menuno;
            showDialog("editframe", "/Module/Edit?type=edit&a=" + Math.random() + "&menuno=" + menuno, {
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
                }]
            });
        }
        else {
            $.messager.alert('警告', '请先选择一个角色');
        }
    });
}

function initShowPower() {
    $("#btnPower").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var menuno = row.menuno;
            showDialog("powerframe", "", {});
            $("#powerframeinfo").show();
            $("#powerinfo").datagrid({
                url: '/Module/Power',
                queryParams: { menuno: menuno, rnd: Math.random() },
                cls: 'table-wrap',
                toolbar: [{
                    iconCls: 'icon-remove',
                    text:'删除权限',
                    handler: function () {
                        var powerrow = $("#powerinfo").datagrid("getSelected");
                        if (powerrow) {
                            var pkid = powerrow.pkid;
                            $.post("/Module/DeletePower", { pkid: pkid, rnd: Math.random() }, function (rsp) {
                                if (rsp && rsp.success === true) {
                                    var _index = $("#powerinfo").datagrid("getRowIndex", powerrow);
                                    $("#powerinfo").datagrid("deleteRow", _index);
                                }
                                else {
                                    if (rsp) {
                                        $.messager.alert("警告", decodeURIComponent(rsp.message));
                                    }
                                    else {
                                        $.messager.alert("警告", "提交失败");
                                    }
                                }
                            }, "json");
                        }
                        else {
                            $.messager.alert("警告", "请选择一个权限进行删除");
                        }
                    }
                }]
,
                rownumbers: true,
                singleSelect: true,
                autoRowHeight: false,
                pagination: false,
                idField: 'powerno',
                columns: [[
                    { field: 'powerno', title: '权限代码', width: 100 },
                    { field: 'powername', title: '权限名称', width: 100 }
                ]]
            });
        }
        else {
            $.messager.alert('警告', '请先选择一个角色');
        }
    });

    $("#btnAddPower").click(function () {
        var powerno = $("#powerno").combogrid("getValue");
        var powername = $("#powerno").combogrid("getText");
        if (powerno == "---") {
            $.messager.alert("警告", "请选择权限");
            return;
        }
        var row = $("#list").treegrid("getSelected");
        $.post("/Module/InsertPower", {
            powerno: powerno,
            powername: powername,
            menuno: row.menuno
        }, function (data) {
            if (data && data.success === true) {
                $("#powerinfo").datagrid('appendRow', data.rows);
            }
            else {
                if (data) {
                    $.messager.alert("警告", decodeURIComponent(data.message));
                }
                else {
                    $.messager.alert("警告", "提交失败");
                }
            }
        }, "json");
    });
}

function initShowObject() {
    $("#btnForObject").click(function () {
        var row = $("#list").datagrid("getSelected")
        if (row) {
            var menuno = row.menuno;
            showDialog("objectframe", "", {});
            $("#objectframeinfo").show();
            $("#objectinfo").datagrid({
                url: '/Module/ForObject',
                queryParams: { menuno: menuno, rnd: Math.random() },
                cls: 'table-wrap',
                toolbar: [{
                    iconCls: 'icon-remove',
                    text: '删除对象',
                    handler: function () {
                        var objectrow = $("#objectinfo").datagrid("getSelected");
                        if (objectrow) {
                            var pkid = objectrow.pkid;
                            $.post("/Module/DeleteObject", { pkid: pkid, rnd: Math.random() }, function (rsp) {
                                if (rsp && rsp.success === true) {
                                    var _index = $("#objectinfo").datagrid("getRowIndex", objectrow);
                                    $("#objectinfo").datagrid("deleteRow", _index);
                                }
                                else {
                                    if (rsp) {
                                        $.messager.alert("警告", decodeURIComponent(rsp.message));
                                    }
                                    else {
                                        $.messager.alert("警告", "提交失败");
                                    }
                                }
                            }, "json");
                        }
                        else {
                            $.messager.alert("警告", "请选择一个权限进行删除");
                        }
                    }
                }]
,
                rownumbers: true,
                singleSelect: true,
                autoRowHeight: false,
                pagination: false,
                idField: 'forobject',
                columns: [[
                    { field: 'forobject', title: '对象代码', width: 100 }
                ]]
            });
        }
        else {
            $.messager.alert('警告', '请先选择一个角色');
        }
    });

    $("#btnAddObject").click(function () {
        var objectno = $("#objectno").val();
        if (objectno == "") {
            $.messager.alert("警告", "请输入权限对象");
            return;
        }
        var row = $("#list").treegrid("getSelected");
        $.post("/Module/InsertObject", {
            forobject: objectno,
            menuno: row.menuno
        }, function (data) {
            if (data && data.success === true) {
                $("#objectinfo").datagrid('appendRow', data.rows);
            }
            else {
                if (data) {
                    $.messager.alert("警告", decodeURIComponent(data.message));
                }
                else {
                    $.messager.alert("警告", "提交失败");
                }
            }
        }, "json");
    });
}

function initDelete() {
    $("#btnDelete").click(function () {
        var row = $("#list").datagrid("getSelected");
        if (row) {
            $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
                if (r) {
                    $.post("/module/delete", { menuno: row.menuno, rnd: Math.random() }, function (data) {
                        if (data && data.success === true) {
                            $("#list").treegrid("remove", row.menuno);
                        }
                        else {
                            if (data) {
                                $.messager.alert("警告", decodeURIComponent(data.message));
                            }
                            else {
                                $.messager.alert("警告", "删除失败");
                            }
                        }
                    }, "json");
                }
            });            
        }
        else {
            $.messager.alert('警告', '请先选择一个角色');
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
        if ($("#editform").attr("action").toLowerCase() == "/module/insert") {
            $("#list").treegrid('append', {
                parent: data.rows.parentno,
                data: [data.rows]
            });
        }
        else {
            $("#list").treegrid('update', {
                id: data.rows.menuno,
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