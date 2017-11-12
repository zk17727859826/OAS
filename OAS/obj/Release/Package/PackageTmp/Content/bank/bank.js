function initQuery() {
    $("#btnQuery").click(function () {
        var title = $("#title").val();
        var qtype = $("#qtype").combobox("getValue");
        var ynpic = $("#ynpic").combobox("getValue");
        var ynanimal = $("#ynanimal").combobox("getValue");
        queryBanks(title, qtype, ynpic, ynanimal);
    });

    queryBanks("", "", "", "");
}

function queryBanks(title, qtype, ynpic, ynanimal) {
    $("#list").datagrid({
        url: '/BankDesc/Query',
        queryParams: { title: title, qtype: qtype, ynpic: ynpic, ynanimal: ynanimal, rnd: Math.random() },
        rownumbers: true,
        fit: true,
        singleSelect: true,
        autoRowHeight: false,
        idField: "id",
        pagination: true,
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

                    if (rsp.data.answerpicpath) {
                        $("#bankanswerpath").attr("src", "/Resource/BankImages" + decodeURIComponent(rsp.data.answerpicpath)).show();
                    }
                    else {
                        $("#bankanswerpath").hide();
                    }
                }
            }, "json");
        },
        columns: [[
            { field: 'id', title: '编号',width:'70',align:'center' },
            { field: 'qtype', title: '题型', width: '70', align: 'center' },
            {
                field: 'errortype', title: '是否在重点错题库', width: '70', align: 'center', formatter: function (value, row, index) {
                    if (row.errortype=="A") {
                        return "Y";
                    } else {
                        return "";
                    }
                }
            },
            {
                field: 'picpath', title: '图片/动画', align: 'center', formatter: function (value, row, index) {
                    if (row.picpath) {
                        return '图片';
                    } else if (row.animepath) {
                        return '动画';
                    }
                    else {
                        return '';
                    }
                }
            },
            { field: 'title', title: '标题' }
        ]]
    });
}

function initRefresh() {
    $("#btnRefresh").click(function () {
        $("#list").datagrid("reload");
    });
}

function initShowOther() {
    $("#btnOther").click(function () {
        var row = $("#list").datagrid("getSelected");
        if (row) {
            $.post("/BankDesc/seterror", { id: row.id, rnd: Math.random() }, function (data) {
                if (data && data.success === true) {
                    $("#list").datagrid('updateRow', {
                        index: $("#list").datagrid("getRowIndex", row),
                        row: data.rows
                    });
                    if (data.rows.errortype == "A") {
                        $.messager.alert("提示", "设置成功");
                    }
                    else {
                        $.messager.alert("提示", "取消成功");
                    }
                }
                else {
                    if (data) {
                        $.messager.alert("警告", decodeURIComponent(data.message));
                    }
                    else {
                        $.messager.alert("警告", "设置失败");
                    }
                }
            }, "json")
        }
        else {
            $.messager.alert('警告', '请先选择一个角色');
        }
    });
}

function initShowAdd() {
    $("#btnShowAdd").click(function () {
        showDialog("editframe", "/BankDesc/Edit?a=" + Math.random(), {
            height:500,
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
            var id = row.id;
            showDialog("editframe", "/BankDesc/Edit?a=" + Math.random() + "&id=" + id, {
                height: 500,
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
                    $.post("/BankDesc/delete", { id: row.id, rnd: Math.random() }, function (data) {
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
        if ($("#editform").attr("action").toLowerCase() == "/bankdesc/add") {
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
        $("#list").datagrid("selectRecord", data.rows.id);
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

function initAnswerSelect() {    
    $(document).delegate(":checkbox[name='answeroption']", "click", function () {
        var _qtype = $("#eqtype").combobox("getValue");
        if (_qtype == "判断题" || _qtype == "单选题") {
            $(":checkbox[name='answeroption']").prop("checked", false);
            $(this).prop("checked", true);
        }
        var answers = [];
        $(":checkbox[name='answeroption']:checked").each(function () {
            answers.push($(this).val());
        });

        $("#answer").val(answers.join(","));
    });
}

function initChexingSelect() {
    $(document).delegate(":checkbox[name='belongtypeoption']", "click", function () {
        var belongtypes = [];
        $(":checkbox[name='belongtypeoption']:checked").each(function () {
            belongtypes.push($(this).val());
        });

        $("#belongtype").val(belongtypes.join(","));
    });
}

function initImageSelected() {
    $(document).delegate(".imagepath", "click", function () {
        var _this = this;
        showDialog("imageframe", null, {
            height: 500,
            width: 700,
            buttons: [{
                text: '保存',
                iconCls: 'icon-ok',
                handler: function () {
                    var size = $(".picitem.selected").size();
                    if (size == 0) {
                        $.messager.alert("警告", "请选择一个图片");
                    }
                    else {
                        $(_this).val($(".picitem.selected").attr("data-path"));
                        hideDialog("imageframe");
                    }
                }
            }, {
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    hideDialog("imageframe");
                }
            }]
        });

        $.post("/Pic/Folder?rnd=" + Math.random(), {}, function (data) {
            if (data && data.success === true) {
                $("#imgfolder").tree({
                    data: data.rows,
                    animate: true,
                    lines: true,
                    onSelect: function (node) {
                        queryFiles(node.id);
                    }
                });
            }
            else {
                if (data) {
                    $.messager.alert("警告", decodeURIComponent(data.message));
                }
                else {
                    $.messager.alert("警告", "获得图片信息失败");
                }
            }
        }, "json");
    });
}

function queryFiles(path) {
    $.post("/Pic/Files", { imagefile: path, rnd: Math.random() }, function (rsp) {
        if (rsp && rsp.success === true) {
            $(".picitem").remove();
            var html = "";
            for (var i = 0; i < rsp.rows.length; i++) {
                var row = rsp.rows[i];
                html += "<li class='picitem' data-path='" + row.Item1 + "'><img src='/Resource/BankImages" + row.Item1 + "'><div>" + row.Item2 + "</div></li>";
            }
            $("#imagefiles").prepend($(html));
        }
        else {
            if (rsp) {
                $.messager.alert("警告", decodeURIComponent(rsp.message));
            }
            else {
                $.messager.alert("警告", "获得图片信息失败");
            }
        }
    }, "json");
}

function initPicItemSelected() {
    $(document).delegate(".picitem", "click", function () {
        $(".picitem").removeClass("selected");
        $(this).addClass("selected");
    });
}

function chooseAnswerOptions(value) {
    $(".answeroption").prop("checked", false).parent().hide();
    $("#answer").val("");
    switch (value) {
        case "单选题":
            $(".single").parent().show();
            break;
        case "多选题":
            $(".multi").parent().show();
            break;
        case "判断题":
            $(".judge").parent().show();
            break;
        default:
            break;
    }
}