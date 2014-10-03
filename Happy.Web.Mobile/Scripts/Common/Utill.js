var Utill = {
    //Message. 
    //Utill.Alert('메세지', S:성공, E:에러, I:알림);
    Alert: function (message, type) {
        PNotify.prototype.options.styling = "jqueryui";
        PNotify.prototype.options.history = false;
        if (type == "S") {
            new PNotify({
                title: false,
                text: message,
                type: 'success',
                hide: false
            });
        }
        if (type == "E") {
            new PNotify({
                title: false,
                text: message,
                type: 'error',
                hide: false
            });
        }
        if (type == "I") {
            new PNotify({
                title: false,
                text: message,
                type: 'info',
                hide: false
            });
        }
    },
    //MobileDevice Check
    IsMobile: function () {
        var isMobile = {
            Android: function () {
                return navigator.userAgent.match(/Android/i);
            },
            BlackBerry: function () {
                return navigator.userAgent.match(/BlackBerry/i);
            },
            iOS: function () {
                return navigator.userAgent.match(/iPhone|iPad|iPod/i);
            },
            Opera: function () {
                return navigator.userAgent.match(/Opera Mini/i);
            },
            Windows: function () {
                return navigator.userAgent.match(/IEMobile/i);
            },
            any: function () {
                return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
            }
        }
        if (isMobile.any()) {
            return true;
        }
        else {
            return false;
        }
    },
    //TextBox 빈값 체크
    Nullcheck: function (id, message) {
        var text = $('#' + id).val();
        if (text == '') {
            Utill.Alert(message, 'I');
            return false;
        }
        else {
            return true;
        }
    },
    //레이어 팝업 프레임.
    OpenPopupFrame: function (url, title, width, height) {
        var windowID = $(url.split('/')).last()[0].split('.')[0];
        var $dialog = $("#" + windowID);

        $('body').after('<iframe id="' + windowID + '" style="padding:0;" src="' + url + '">  </iframe>');
        $dialog = $("#" + windowID)
        $dialog.dialog(
         {
             autoOpen: false,
             title: title,
             position: 'center',
             sticky: false,
             width: width,
             height: height,
             draggable: true,
             resizable: false,
             modal: true,
             close: function () {
                 $(this).dialog('destroy');
                 $("#" + windowID).remove();
             }
         });
        $dialog.load(function () {
            $dialog.dialog('open');
            $dialog.css("width", "100%");
        });
    },
    //레이어팝업 호출
    OpenPopup: function (id, title, width, height) {
        var $dialog = $("#" + id);
        $dialog.dialog(
         {
             autoOpen: false,
             title: title,
             position: 'center',
             sticky: false,
             width: width,
             height: height,
             draggable: true,
             resizable: false,
             modal: true,
             close: function () {
                 $(this).dialog('destroy');
             }
         });
        $dialog.dialog('open');
    },
    // replace all
    ReplaceAll: function (fulltxt, oTxt, nTxt) {
        return fulltxt.split(oTxt).join(nTxt);
    },
    //Ajax
    Ajax: function (url, data) {
        var returnData;
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            async: false,
            success: function (data) {
                returndata = data;
            },
            error: function (e) {
                console.log(e);
                Utill.Alert("통신에러가 발생했습니다.", "E");
            }
        });
        return returndata;
    }
}

// 암호화 모듈
// TomochanSecurity.Encription("aaaa")
eval(function (p, a, c, k, e, d) { e = function (c) { return (c < a ? '' : e(parseInt(c / a))) + ((c = c % a) > 35 ? String.fromCharCode(c + 29) : c.toString(36)) }; if (!''.replace(/^/, String)) { while (c--) { d[e(c)] = k[c] || e(c) } k = [function (e) { return d[e] }]; e = function () { return '\\w+' }; c = 1 }; while (c--) { if (k[c]) { p = p.replace(new RegExp('\\b' + e(c) + '\\b', 'g'), k[c]) } } return p }('5 s={U:m(z){5 t=\'\';5 A=z.l;H(5 i=0;i<A;i++){5 F=z.J(i,i+1);d(i!=A-1){t+=F.b(0)+s.L()}v{t+=F.b(0)}}x s.N(t)},L:m(){5 E="/=+T";5 K=1;5 C=\'\';H(5 i=0;i<K;i++){5 D=M.Q(M.S()*E.l);C+=E.J(D,D+1)}x C},N:m(8){5 e="Z+/=";5 p="";5 q,f,j,B,G,u,k;5 i=0;8=s.I(8);10(i<8.l){q=8.b(i++);f=8.b(i++);j=8.b(i++);B=q>>2;G=((q&3)<<4)|(f>>4);u=((f&X)<<2)|(j>>6);k=j&w;d(P(f)){u=k=O}v d(P(j)){k=O}p=p+e.o(B)+e.o(G)+e.o(u)+e.o(k)}x p},I:m(h){h=h.W(/\\r\\n/g,"\\n");5 7="";H(5 n=0;n<h.l;n++){5 c=h.b(n);d(c<y){7+=a.9(c)}v d((c>11)&&(c<Y)){7+=a.9((c>>6)|R);7+=a.9((c&w)|y)}v{7+=a.9((c>>12)|V);7+=a.9(((c>>6)&w)|y);7+=a.9((c&w)|y)}}x 7}}', 62, 65, '|||||var||utftext|input|fromCharCode|String|charCodeAt||if|_keyStr|chr2||string||chr3|enc4|length|function||charAt|output|chr1||TomochanSecurity|encText|enc3|else|63|return|128|text|count|enc1|randomstring|rnum|chars|utext|enc2|for|Utf8Encode|substring|string_length|RandText|Math|Encode64|64|isNaN|floor|192|random|abcdefghijklmnopqrstuvwxyz|Encription|224|replace|15|2048|ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789|while|127|'.split('|'), 0, {}))


