//For Item
$("body").on("input", "#ip_itemCode", function (e) {

});

$("body").on("keyup", "#ip_itemCode", function (e) {

});
//Auto complete
function AutoCompleteItemName(urlGetItem) {
    $.ajax({
        type: "POST",
        url: urlGetItem,
        data: '{"itemCode":"test"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var itemCode = [];
            for (var i = 0; i < data.GetItemFG.length; i++) {
                itemCode.push(data.GetItemFG[i].itemCode);
            }

            $('.i-itemcode').autocomplete({
                //delay: 100,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(itemCode, request.term);
                    response(results.slice(0, 10));
                }, select: function (event, ui) {

                    for (var i = 0; i < data.GetItemFG.length; i++) {
                        if (data.GetItemFG[i].itemCode == ui.item.value) {
                            $('.i-itemcode').val(data.GetItemFG[i].itemCode);
                            //GetBOMSF(urlBOMSF, data.GetItemFG[i].itemCode)
                        }

                    }

                }

            });
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(errorThrown);
        }
    });
    return false;
}