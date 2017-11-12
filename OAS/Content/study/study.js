function initQuery() {
    $("#btnQuery").click(function () {
        var xueyh = $("#xueyh").val();
        var xingming = $("#xingming").val();
        $("#list").datagrid({
            url: '/TestRecords/Index',
            queryParams: { xueyh: xueyh, xingming: xingming, rnd: Math.random() },
            cls: 'table-wrap',
            fit: true,
            rownumbers: true,
            singleSelect: true,
            autoRowHeight: false,
            pagination: true,
            columns: [[
                { field: 'xueyh', title: '学员号', width: 70, align: 'center' },
                { field: 'xingming', title: '姓名', width: 70 },
                { field: 'testtype', title: '类型', width: 70, align: 'center' },
                { field: 'projecttype', title: '项目', width: 100 },
                { field: 'kemu', title: '科目', width: 100, align: 'center', formatter: showKemu },
                { field: 'source', title: '来源', width: 70, align: 'center', formatter: showSource },
                { field: 'totalnum', title: '总记录数', width: 70, align: 'right' },
                { field: 'oknum', title: '正确数', width: 70, align: 'right' },
                { field: 'createdate', title: '创建日期', formatter: formatDate },
            ]]
        });
    }).trigger("click");
}

function showSource(value, row, index) {
    var html = "";
    switch (value) {
        case "1":
            html = "网页";
            break;
        case "2":
            html = "APP";
            break;
        case "3":
            html = "小程序";
            break;
        default:
            html = "";
    }
    return html;
}