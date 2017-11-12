var kemuStr = {
    "KM1": "科目一",
    "KM4": "科目四",
    "KMA": "客车",
    "KMB": "货车",
}

/*
 * 日期类的格式化
 */
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month 
        "d+": this.getDate(), //day 
        "h+": this.getHours(), //hour 
        "m+": this.getMinutes(), //minute 
        "s+": this.getSeconds(), //second 
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
        "S": this.getMilliseconds() //millisecond 
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

function formatDate(value) {
    if (value) {
        value.replace(/Date\([\d+]+\)/, function (a) { eval('d = new ' + a) });
        return d.format("yyyy/MM/dd hh:mm:ss");
    }
    else {
        return "";
    }
}

function showKemu(value, row, index) {
    return kemuStr[value] || "";
}

function showDialog(dialogid, url, option) {
    var _option = {
        width: 400,
        height: 300,
        title: '对话框',
        closed: false,
        cache: false,
        modal: true,
        iconCls: 'icon-save'
    };
    if (url) {
        _option["href"] = url;
    }

    _option = $.extend(_option, option);

    $("#" + dialogid).dialog(_option)
}

function hideDialog(dialogid) {
    $("#" + dialogid).dialog("close")
}