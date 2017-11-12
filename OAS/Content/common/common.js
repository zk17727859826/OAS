function initPower(selector) {
    $(selector).combogrid({
        panelWidth: 350,
        width: 100,
        value: '---',
        idField: 'powerno',
        textField: 'powername',
        url: '/Common/Power',
        columns: [[
            { field: 'powerno', title: '权限编号'},
            { field: 'powername', title: '权限名称'},
            { field: 'memo', title: '权限描述'}
        ]]
    });
}