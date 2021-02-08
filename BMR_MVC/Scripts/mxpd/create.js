//Create by Por 8/6/2020

//For Item
$("body").on("input", "#i_cmxpd_itemcode", function (e) {

});

$("body").on("keyup", "#i_cmxpd_itemname", function (e) {
  
});

//For Batch
$("body").on("keyup", "#i_cmxpd_batchsize", function (e) {
    // skip for arrow keys
    if (event.which >= 37 && event.which <= 40) return;
    // format number
    $(this).val(function (index, value) {
        return value.replace(/\D/g, "") .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    });
});

//For Lot
$("body").on("keyup", "#i_cmxpd_lot", function (e) {
    $(this).val($(this).val().toUpperCase());
});

//Auto complete
function AutoCompleteItemName(urlGetItem,urlBOMSF) {
    $.ajax({
        type: "POST",
        url: urlGetItem,
        data: '{"itemName":"test"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var itemName = [];
            for (var i = 0; i < data.GetItemFG.length; i++) {
                itemName.push(data.GetItemFG[i].itemName);
            }

            $('.i-itemname').autocomplete({
                //delay: 100,
                source: function (request, response) {
                    var results = $.ui.autocomplete.filter(itemName, request.term);
                    response(results.slice(0, 10));
                }, select: function (event, ui) {

                    for (var i = 0; i < data.GetItemFG.length; i++) {
                        if (data.GetItemFG[i].itemName == ui.item.value) {
                            $('.i-itemcode').val(data.GetItemFG[i].itemCode);
                            GetBOMSF(urlBOMSF, data.GetItemFG[i].itemCode)
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

//Get bom sf after select autocomplete itemname,itemcode from partialview
function GetBOMSF(urlBOMSF, itemCode) {
    var url = urlBOMSF;
    url = url.replace('__itemCode__', itemCode);
    $.ajax({
        type: 'POST',
        url: url,
        datatype: "html",
        success: function (data) {
            $('.t-bomsf').html(data)
        }
    });
}