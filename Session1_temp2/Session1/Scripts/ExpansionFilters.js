$.validator.addMethod("checkdata", function (value, element, param) {

    //無值不辨斷
    if (value == false) {
        return true;
    }

    //處理日期
    var newDate = value.replace(/-/g,'/'); // 變成"2012/01/01 12:30:10";
    new Date(newDate); //可順利在不同瀏覽器使用
    var Today = new Date();

    //
    if (newDate > Today) {
        return false;
    }
    else {
        return true;
    }
});
$.validator.unobtrusive.adapters.addSingleVal("checkdata");

