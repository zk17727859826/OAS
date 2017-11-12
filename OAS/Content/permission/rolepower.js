var selectedinfo = {
    roleno: "",
    menuno: "",
    menunoChecked: false,
    powerno: "-1",
    powernoChecked: false
};
function queryRole() {
    $("#tblrole").datagrid({
        url: '/RolePower/role',
        queryParams: { rnd: Math.random() },
        border: false,
        fit: true,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        autoRowHeight: false,
        onSelect: function (index, row) {
            queryModule(row.roleno);
            selectedinfo.roleno = row.roleno;
            selectedinfo.menuno = "";
            selectedinfo.menunoChecked = false;
            selectedinfo.powerno = "";
            selectedinfo.powernoChecked = false;

            //清空前面选则的权限及对象
            queryModulePower(selectedinfo.roleno, "-");
            selectedinfo.powerno = "-1";
            queryRoleObjects(false);
        },
        columns: [[
            { field: 'roleno', title: '角色编号' },
            { field: 'rolename', title: '角色名称' }
        ]]
    });
}

function queryModule(roleno) {
    $("#treeModule").tree({
        url: '/RolePower/Module?roleno=' + roleno,
        rownumbers: true,
        checkbox: true,
        animate: true,
        lines: true,
        onSelect: function (node) {
            selectedinfo.menuno = node.id;
            var checkedNodes = $('#treeModule').tree('getChecked', ['checked', 'indeterminate']);
            selectedinfo.menunoChecked = false;
            for (var i = 0; i < checkedNodes.length; i++) {
                if (selectedinfo.menuno == checkedNodes[i].id) {
                    selectedinfo.menunoChecked = true;
                    break;
                }
            }
            queryModulePower(selectedinfo.roleno, node.id);
            selectedinfo.powerno = "-1";
            selectedinfo.powernoChecked = false;
            queryRoleObjects(false);
        },
        onCheck: function (node, checked) {
            if (node.id == selectedinfo.menuno) {
                selectedinfo.menunoChecked = checked;
                $("#tblModulePower").parent().find(".datagrid-header input[type='checkbox']").prop("disabled", !checked);
                $("#tblModulePower").parent().find(".datagrid-body input[type='checkbox']").prop("disabled", !checked);

                $("#flRoleObjects :checkbox").prop("disabled", !checked);
            }
        },
        columns: [[
            { field: 'menuno', title: '菜单编号', width: 100 },
            { field: 'menuname', title: '菜单名称', width: 100 }
        ]]
    });
}

function saveRoleModule() {
    var nodes = $('#treeModule').tree('getChecked', ['checked', 'indeterminate']);
    var rolepower = [];
    for (var i = 0; i < nodes.length; i++) {
        rolepower.push({
            roleno: selectedinfo.roleno,
            menuno: nodes[i].id
        });
    }

    powers = getRolePowers();
    objects = getRoleObjects();

    $.post("/RolePower/InserRoleModule", {
        info: JSON.stringify(rolepower),
        roleno: selectedinfo.roleno,
        menuno: selectedinfo.menuno,
        powerno: selectedinfo.powerno,
        rolepowers: powers.join(','),
        roleobjects: objects.join(','),
        rnd: Math.random()
    }, function (rsp) {
        if (rsp && rsp.success === true) {
            $.messager.alert("提示", "保存成功");
        }
        else {
            if (rsp) {
                $.messager.alert("警告", decodeURIComponent(rsp.message));
            }
            else {
                $.messager.alert("警告", "保存失败");
            }
        }
    }, "json");
}

function queryModulePower(roleno, menuno) {
    $("#tblModulePower").datagrid({
        url: '/RolePower/RolePowers',
        queryParams: { menuno: menuno, roleno: roleno, rnd: Math.random() },
        border: false,
        fit: true,
        fitColumns: true,
        selectOnCheck: false,
        checkOnSelect: false,
        rownumbers: true,
        singleSelect: true,
        autoRowHeight: false,
        onLoadSuccess: function (data) {
            var $tblmodule = $("#tblModulePower");
            if (!selectedinfo.menunoChecked) {
                $tblmodule.parent().find(".datagrid-header input[type='checkbox']").prop("disabled", true);
                $tblmodule.parent().find(".datagrid-body input[type='checkbox']").prop("disabled", true);
            }

            var rowData = data.rows;
            $.each(rowData, function (idx, val) {//遍历JSON  
                if (val.haspower != 0) {
                    $tblmodule.datagrid("checkRow", idx);//如果数据行为已选中则选中改行  
                }
            });
        },
        onSelect: function (index, row) {
            selectedinfo.powerno = row.powerno;
            selectedinfo.powernoChecked = false;
            var checkedRows = $("#tblModulePower").datagrid("getChecked");
            for (var i = 0; i < checkedRows.length; i++) {                
                if (checkedRows[i].powerno == selectedinfo.powerno) {
                    var _index = $("#tblModulePower").datagrid("getRowIndex", checkedRows[i]);
                    var _checkedbox = $("#tblModulePower").parent().find(".datagrid-body input[type='checkbox']")[_index];
                    if ($(_checkedbox).prop("disabled") == false) {                        
                        selectedinfo.powernoChecked = true;
                        break;
                    }                    
                }
            }
            queryRoleObjects(selectedinfo.powernoChecked);
        },
        onCheck: function (index, row) {
            if (row.powerno == selectedinfo.powerno) {
                $("#flRoleObjects :checkbox").prop("disabled", false);
            }
        },
        onUncheck: function (index, row) {
            if (row.powerno == selectedinfo.powerno) {
                $("#flRoleObjects :checkbox").prop("disabled", true);
            }
        },
        columns: [[
            { field: 'haspower', title: '选', checkbox: true },
            { field: 'powerno', title: '权限编号' },
            { field: 'powername', title: '权限名称' },
            { field: 'memo', title: '权限描述' }
        ]]
    });
}

function getRolePowers() {
    var $powertable = $("#tblModulePower");
    var rows = $powertable.datagrid("getChecked");
    var rolepower = [];
    for (var i = 0; i < rows.length; i++) {
        var _index = $powertable.datagrid("getRowIndex", rows[i]);
        var _checked = $powertable.parent().find(".datagrid-body :checkbox").eq(_index).prop("disabled");
        if (!_checked) {
            rolepower.push(rows[i].powerno);
        }
    }
    return rolepower;
}

function getRoleObjects() {
    var roleobjects = [];
    $("#flRoleObjects :checkbox:checked").each(function () {
        if ($(this).prop("checked")) {
            roleobjects.push($(this).val());
        }
    });
    return roleobjects;
}

function queryRoleObjects(isdisabled) {
    $.post("/RolePower/RoleObjects", {
        roleno: selectedinfo.roleno,
        menuno: selectedinfo.menuno,
        powerno: selectedinfo.powerno,
        rnd: Math.random()
    }, function (rsp) {
        if (rsp && rsp.success === true) {
            var html = "";
            for (var i = 0; i < rsp.rows.length; i++) {
                html += "<div class='objectitem'><label><input type='checkbox' " + (rsp.rows[i].haspower != 0 ? "checked" : "") + " value='" + rsp.rows[i].forobject + "' />" + rsp.rows[i].forobject + "</label></div>";
            }

            $("#flRoleObjects").html(html);
            if (!isdisabled) {
                $("#flRoleObjects :checkbox").prop("disabled", true);
            }
        }
        else {
            if (rsp) {
                $.messager.alert("警告", decodeURIComponent(rsp.message));
            }
            else {
                $.messager.alert("警告", "保存失败");
            }
        }
    }, "json");
}