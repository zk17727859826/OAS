function initQueryFolder() {
    $.post("/Pic/Folder?rnd=" + Math.random(), {}, function (data) {
        if (data && data.success === true) {
            $("#treeFolder").tree({
                data: data.rows,
                animate: true,
                lines: true,
                onSelect: function (node) {
                    queryFiles(node.id);
                },
                onLoadSuccess: function (node, data) {
                    var nodes = $("#treeFolder").tree("getRoots");
                    if (nodes.length > 0) {
                        $("#treeFolder").tree("select", nodes[0].target);
                    }
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
}

function queryFiles(path) {
    $.post("/Pic/Files", { imagefile: path, rnd: Math.random() }, function (rsp) {
        if (rsp && rsp.success === true) {
            $(".picitem").remove();
            var html = "";
            for (var i = 0; i < rsp.rows.length; i++) {
                var row = rsp.rows[i];
                html += "<li class='picitem'><span class='closeflag' data-path='"+row.Item1+"'>×</span><img src='/Resource/BankImages/" + row.Item1 + "'><div>" + row.Item2 + "</div></li>";
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

function initUploadFile() {
    // 初始化Web Uploader
    uploader = WebUploader.create({

        // 选完文件后，是否自动上传。
        auto: true,

        // swf文件路径
        swf: '/Content/webuploader-0.1.5/Uploader.swf',

        // 文件接收服务端。
        server: '/Pic/UploadImage',

        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: '#addfile',

        // 只允许选择图片文件。
        accept: {
            title: 'Images',
            extensions: 'mp4,gif,jpg,jpeg,bmp,png'
            //extensions: 'gif,jpg,jpeg,bmp,png,*'//,
            //mimeTypes: 'image/*'
        }
    });

    uploader.on('fileQueued', function (file) {
        var $li = $(
            '<li id="' + file.id + '" class="picitem thumbnail"><span class="closeflag">×</span>' +
                '<img>' +
                '<div>' + file.name + '</div><div class="uploadprogress"><div class="progressline"></div></div><p class="state"></p>' +
            '</li>'
            ),
        $img = $li.find('img');

        $addflag = $(".picadd");
        // $list为容器jQuery实例
        $addflag.before($li);

        // 创建缩略图
        // 如果为非图片文件，可以不用调用此方法。
        // thumbnailWidth x thumbnailHeight 为 100 x 100
        var thumbnailWidth = 100;
        var thumbnailHeight = 160;
        uploader.makeThumb(file, function (error, src) {
            if (error) {
                $img.replaceWith('<span>不能预览</span>');
                return;
            }

            $img.attr('src', src);
        }, thumbnailWidth, thumbnailHeight);
    });

    // 文件上传过程中创建进度条实时显示。
    uploader.on('uploadProgress', function (file, percentage) {
        var $li = $('#' + file.id);
        var $percent = $li.find('.uploadprogress .progressline');

        $li.find('p.state').text('上传中');
        $percent.css('width', percentage * 100 + '%');
    });

    uploader.on('uploadSuccess', function (file, response) {
        if (response && response.success === true) {
            $('#' + file.id).find('p.state').text('已上传');
            $('#' + file.id).find(".closeflag").attr("data-path", response.file);
        }
        else {
            var msg = "上传失败";
            if (response) {
                msg = decodeURIComponent(msg);
            }

            $('#' + file.id).find('p.state').text(msg);
        }        

        //$('#' + file.id).find('.uploadprogress').fadeOut(2000);
    });

    uploader.on('uploadError', function (file, reason) {
        $('#' + file.id).find('p.state').text(reason);
    });

    uploader.on('uploadBeforeSend', function (object, data, headers) {
        data["folder"] = $("#treeFolder").tree("getSelected").id;
    });
}

function initShowAddFolder(){
    $("#btnShowAdd").click(function () {
        $(".isadd").show();
        $(":radio[name='level']").prop("checked", false);
        $("#typename").val("");
        showDialog("addFrame", "", {
            height: 150,
            width: 300,
            buttons: [{
                text: '保存',
                iconCls: 'icon-ok',
                handler: function () {
                    var node = $("#treeFolder").tree("getSelected");
                    var oldfolder = "";
                    if (node) {
                        oldfolder = node.id;
                    }

                    var level = $(":radio[name='level']:checked").val();
                    if (!level) {
                        if (node) {
                            $.messager.alert("警告", "请选择级别");
                            return;
                        }
                    }
                    var folder = $("#typename").val();
                    if (!folder) {
                        $.messager.alert("警告", "请输入类型");
                        return;
                    }
                    
                    $.post("/Pic/InsertFolder", { level: level, oldfolder: oldfolder, folder: folder }, function (rsp) {
                        if (rsp && rsp.success === true) {
                            hideDialog("addFrame");
                            $.messager.alert("提示", "提交成功");
                            var _optype = level == "2" ? "append" : "insert";
                            var _target = null;
                            if (node) {
                                if (level == "2") {
                                    _target = node.target;
                                }
                                else {
                                    var _parent = $("#treeFolder").tree("getParent", node.target);
                                    if (_parent) {
                                        var _children=$("#treeFolder").tree("getChildren", _parent.target);
                                        _target = _children.eq(_children.length - 1).target;
                                    }
                                }
                            }
                            $("#treeFolder").tree(_optype, {
                                parent: node ? node.target : null,
                                data: {
                                    id: rsp.path,
                                    text: folder
                                }
                            });
                        }
                        else {
                            if (rsp) {
                                $.messager.alert("警告", rsp.message);
                            }
                            else {
                                $.messager.alert("警告", "提交失败");
                            }
                        }
                    }, "json");
                }
            }, {
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    hideDialog("addFrame");
                }
            }]
        });
    });
}

function initShowEditFolder() {
    $("#btnShowEdit").click(function () {
        var node = $("#treeFolder").tree("getSelected");
        if (!node) {
            $.messager.alert("警告", "请选择要编辑的图片类型");
            return;
        }

        $(".isadd").hide();
        $("#typename").val("");
        showDialog("addFrame", "", {
            height: 150,
            width: 300,
            buttons: [{
                text: '保存',
                iconCls: 'icon-ok',
                handler: function () {
                    var folder = $("#typename").val();
                    if (!folder) {
                        $.messager.alert("警告", "请输入类型");
                        return;
                    }
                    $.post("/Pic/UploadFolder", { folder: node.id, newname: folder }, function (rsp) {
                        if (rsp && rsp.success === true) {
                            $("#treeFolder").tree("update", {
                                target: node.target,
                                id: rsp.path,
                                text: folder
                            });
                            hideDialog("addFrame");
                        }
                        else {
                            if (rsp) {
                                $.messager.alert("警告", decodeURIComponent(rsp.message));
                            }
                            else {
                                $.messager.alert("警告", "删除失败");
                            }
                        }
                    }, "json");
                }
            }, {
                text: '关闭',
                iconCls: 'icon-cancel',
                handler: function () {
                    hideDialog("addFrame");
                }
            }]
        });
    });
}

function initDeleteFolder() {
    $("#btnDelete").click(function () {
        var node = $("#treeFolder").tree("getSelected");
        if (node) {
            $.post("/Pic/DeleteFolder", { folder: node.id }, function (rsp) {
                if (rsp && rsp.success === true) {
                    $("#treeFolder").tree("remove", node.target);
                }
                else {
                    if (rsp) {
                        $.messager.alert("警告", decodeURIComponent(rsp.message));
                    }
                    else {
                        $.messager.alert("警告", "删除失败");
                    }
                }
            }, "json");
        }
        else {
            $.messager.alert("警告", "请选择要删除的图片类型");
        }
    });
}

function initDeleteFile() {
    $(document).delegate(".closeflag", "click", function () {
        var _this = this;
        var filepath = $(_this).attr("data-path");        
        $.post("/Pic/DeleteFiles", { filepath: filepath }, function (rsp) {
            if (rsp && rsp.success === true) {
                $(_this).parent().remove();
            }
            else {
                if (rsp) {
                    $.messager.alert("警告", decodeURIComponent(rsp.message));
                }
                else {
                    $.messager.alert("警告", "删除失败");
                }
            }
        }, "json");
    });
}

function initRefresh(){
    $("#btnRefresh").click(function () {
        initQueryFolder();
    });
}