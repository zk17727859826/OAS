var selectedinfo = {
    userno: "",
    roleno: "",
    menuno: "",
    powerno: "",
    forobject: ""
};

function initQueryUser() {
    $("#btnQuery").click(function () {
        var keywords = $("#keywords").val();
        $("#userinfo").datagrid({
            url: '/User/Index',
            queryParams: { userno: keywords, rnd: Math.random() },
            fit: true,
            border: false,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: false,
            onSelect: function (index,row) {
                selectedinfo.userno = row.userno;
                queryUserRoles();
            },
            columns: [[
                { field: 'userno', title: '登陆名', width: 100 },
                { field: 'username', title: '姓名', width: 100 }
            ]]
        });
    }).trigger("click");
}

function queryRoles() {
    $("#roleinfo").datagrid({
        url: '/Role/Query',
        queryParams: { rnd: Math.random() },
        fit: true,
        border: false,
        rownumbers: true,
        singleSelect: true,
        checkOnSelect: false,
        selectOnCheck: false,
        autoRowHeight: false,
        idField: "",
        onSelect: function (index, row) {
            selectedinfo.roleno = row.roleno;
            selectedinfo.menuno = "";
            selectedinfo.powerno = "";
            selectedinfo.forobject = "";
            queryMenus();
            queryModulePower();
            $("#flRoleObjects").html("");
        },
        columns: [[
            { field: 'roleno', checkbox: true },
            { field: 'rolename', title: '角色名称' },
            { field: 'memo', title: '角色描述'}
        ]]
    });
}

function queryMenus() {
    $("#treeModule").tree({
        url: '/UserPower/Module?roleno=' + selectedinfo.roleno,
        rownumbers: true,
        animate: true,
        lines: true,
        onSelect: function (node) {
            selectedinfo.menuno = node.id;
            selectedinfo.powerno = "";
            selectedinfo.forobject = "";
            queryModulePower();
            $("#flRoleObjects").html("");
        },
        columns: [[
            { field: 'menuno', title: '菜单编号', width: 100 },
            { field: 'menuname', title: '菜单名称', width: 100 }
        ]]
    });
}

function queryModulePower() {
    $("#tblModulePower").datagrid({
        url: '/UserPower/RolePowers',
        queryParams: { menuno: selectedinfo.menuno, roleno: selectedinfo.roleno, rnd: Math.random() },
        border: false,
        fit: true,
        fitColumns: true,
        rownumbers: true,
        singleSelect: true,
        autoRowHeight: false,
        onSelect: function (index, row) {
            selectedinfo.powerno = row.powerno;
            queryRoleObjects();
        },
        columns: [[
            { field: 'powerno', title: '权限编号' },
            { field: 'powername', title: '权限名称' },
            { field: 'memo', title: '权限描述' }
        ]]
    });
}

function queryRoleObjects(isdisabled) {
    $.post("/UserPower/RoleObjects", {
        roleno: selectedinfo.roleno,
        menuno: selectedinfo.menuno,
        powerno: selectedinfo.powerno,
        rnd: Math.random()
    }, function (rsp) {
        if (rsp && rsp.success === true) {
            var html = "";
            for (var i = 0; i < rsp.rows.length; i++) {
                html += "<div class='objectitem'>" + rsp.rows[i].forobject + "</div>";
            }

            $("#flRoleObjects").html(html);
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

function queryUserRoles() {
    $.post("/UserPower/QueryUserRoles", {
        userno: selectedinfo.userno,
        rnd: Math.random()
    }, function (rsp) {
        if (rsp && rsp.success === true) {
            $("#roleinfo").parent().find(".datagrid-body :checkbox").prop("checked", false);
            for (var i = 0; i < rsp.rows.length; i++) {
                $("#roleinfo").parent().find(".datagrid-body :checkbox[value='" + rsp.rows[i].roleno + "']").prop("checked", true);
            }
        }
        else{
            if (rsp) {
                $.messager.alert("警告", decodeURIComponent(rsp.message));
            }
            else {
                $.messager.alert("警告", "查询失败");
            }
        }
    }, "json");
}

function saveUserRoels() {
    if (selectedinfo.userno == "") {
        $.messager.alert("警告", "请选择一个用户");
        return;
    }
    var rows = $("#roleinfo").datagrid("getChecked");
    var rolenos = [];
    if (rows) {
        for (var i = 0; i < rows.length; i++) {
            rolenos.push(rows[i].roleno);
        }
    }

    $.post("/UserPower/InserUserRoles", {
        userno: selectedinfo.userno,
        rolenos: rolenos.join(','),
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