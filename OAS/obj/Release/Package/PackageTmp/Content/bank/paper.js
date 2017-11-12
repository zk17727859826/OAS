function initQuery() {
    $("#btnQuery").click(function () {
        var papername = $("#papername").val();
        $("#list").datagrid({
            url: '/Paper/Query',
            queryParams: { papername: papername, rnd: Math.random() },
            fit:true,
            rownumbers: true,
            border: false,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            onSelect: function (index, row) {
                $.post("/Paper/QueryPaperBank", { paperid: row.paperid, rnd: Math.random() }, function (rsp) {
                    if (rsp && rsp.success === true) {
                        $("#banklist").datagrid("clearChecked");
                        if (rsp.rows) {
                            for (var i = 0; i < rsp.rows.length; i++) {
                                $("#banklist").datagrid("selectRecord", rsp.rows[i].bankid);
                                var record = $("#banklist").datagrid("getSelected");
                                var index = $("#banklist").datagrid("getRowIndex", record);
                                $("#banklist").datagrid("checkRow", index);
                            }
                            $("#banklist").datagrid("clearSelections");
                        }
                    }
                    else {
                        if (rsp) {
                            $.messager.alert("警告", decodeURIComponent(rsp.message));
                        }
                        else {
                            $.messager.alert("警告", "获得试卷对应题目失败");
                        }
                    }
                }, "json");
            },
            columns: [[
                { field: 'paperid', title: '试卷编号' },
                { field: 'papername', title: '试卷名称' },
                { field: 'editer', title: '编辑人' },
                { field: 'editdate', title: '编辑时间', formatter: formatDate }
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
        showDialog("editframe", "/Paper/Edit?a=" + Math.random(), {
            height: 200,
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
            var sid = row.paperid;
            showDialog("editframe", "/Paper/Edit?a=" + Math.random() + "&paperid=" + sid, {
                height: 200,
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
            $.messager.alert('警告', '请先选择一个章节');
        }
    });
}

function initDelete() {
    $("#btnDelete").click(function () {
        $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
            if (r) {
                var row = $("#list").datagrid("getSelected");
                if (row) {
                    $.post("/Paper/delete", { paperid: row.paperid, rnd: Math.random() }, function (data) {
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
                    $.messager.alert('警告', '请先选择一个角色');
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
        if ($("#editform").attr("action").toLowerCase() == "/paper/add") {
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

function queryAllBank() {
    $("#banklist").datagrid({
        url: '/Paper/bankinfo',
        queryParams: { rnd: Math.random() },
        fit: true,
        rownumbers: true,
        border: false,
        idField: "id",
        checkOnSelect: false,
        selectOnCheck: false,
        singleSelect: true,
        autoRowHeight: false,
        pagination: false,
        onSelect: function (index, row) {
            $.post("/BankDesc/Single/" + row.id, {}, function (rsp) {
                if (rsp && rsp.success === true) {
                    $("#bankid").text(rsp.data.id);
                    $("#bankqtype").text(rsp.data.qtype);
                    $("#banktitle").text(rsp.data.title);
                    $("#bankoptions").html(rsp.data.options.replace(/[\r\n]/g, '<br />'));
                    $("#sectioninfo").html("<span class='secflag'>章节</span>：" + rsp.data.sectionname || "");
                    $("#bankanswer").html("<span class='secflag'>答案</span>：" + rsp.data.answer || "");
                    $("#bankanswerdesc").html("<span class='secflag'>解释</span>：" + rsp.data.answerdesc || "");
                    if (rsp.data.picpath) {
                        $("#bankpicpath").attr("src", "/Resource/BankImages" + decodeURIComponent(rsp.data.picpath)).show();
                    }
                    else if (rsp.data.animepath) {
                        $("#bankanswerpath").attr("src", "/Resource/BankImages" + decodeURIComponent(rsp.data.answerpath)).show();
                    }
                    else {
                        $("#bankpicpath").hide();                        
                    }
                }
            }, "json");
        },
        columns: [[
            { checkbox: true },
            { field: 'id', title: '题目编号' },
            { field: 'title', title: '题目内容' }
        ]]
    });
}

function savePaperQuestions() {
    var selectedRow = $("#list").datagrid("getSelected");
    if (selectedRow) {
        var rows = $("#banklist").datagrid("getChecked");
        var ids = [];
        for (var i = 0; i < rows.length; i++) {
            ids.push(rows[i].id);
        }
        $.post("/Paper/InsertPaperBank", {
            paperid: selectedRow.paperid,
            bankids: ids.join(","),
            rnd: Math.random()
        }, function (rsp) {
            if (rsp && rsp.success === true) {
                $.messager.alert("警告", "保存试卷题目成功");
            }
            else {
                if (rsp) {
                    $.messager.alert("警告", decodeURIComponent(rsp.message));
                }
                else {
                    $.messager.alert("警告", "保存试卷题目失败");
                }
            }
        }, "json");
    }
    else {
        $.messager.alert("警告", "请选择一个试卷");
    }
}