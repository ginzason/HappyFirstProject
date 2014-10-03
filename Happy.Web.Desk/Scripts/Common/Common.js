var Common = {
    GoPage: function (page) {
        var fulllUrl = location.href;
        var arrFullUrl = fulllUrl.split('?');
        var url = arrFullUrl[0];
        var parameter = '';
        if (arrFullUrl.length > 1) {
            var arrParam = arrFullUrl[1].split('&');
            for (var i = 0; i < arrParam.length; i++) {
                var arrDparam = arrParam[i].split('=');
                if (arrDparam[0] == 'page') {
                    parameter += parameter != '' ? '&page=' + page : '?page=' + page;
                }
                else {
                    parameter += parameter != '' ? '&' + arrDparam[0] + '=' + arrDparam[1] : '?' + arrDparam[0] + '=' + arrDparam[1];
                }
            }
            url += parameter;
        }
        else {
            url += '?page=' + page;
        }
        location.href = url;
    }
}