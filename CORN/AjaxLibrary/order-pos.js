

function GetPendingBill(saleInvoiceMasterId) {
    $.ajax
         (
             {
                 type: "POST", //HTTP method
                 url: "frmOrderPOS.aspx/GetPendingBill", //page/method name
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 data: "{'saleInvoiceMasterId':'" + saleInvoiceMasterId + "'}",
                 success: LoadPendingBill
             }
         );
}
function LoadPendingBill(products) {
    products = JSON.stringify(products);
    var result = jQuery.parseJSON(products.replace(/&quot;/g, '"'));
    products = eval(result.d);
    $("#tble-ordered-products").empty();

    for (var i = 0, len = products.length; i < len; ++i) {
        addProductToOrderedProduct(products, i);
    }
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('<%=hfOrderedproducts.ClientID%>').value = tableData;
    setTotals();
}
//--------------------------------------------------------------------------------------------
function loadProductCategory() {
    $.ajax
         (
             {
                 type: "POST", //HTTP method
                 url: "frmOrderPOS.aspx/LoadProductCategroy", //page/method name
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: addCategories
             }
         );
}
function addCategories(categories) {
    categories = JSON.stringify(categories);
    var result = jQuery.parseJSON(categories.replace(/&quot;/g, '"'));
    categories = eval(result.d);
    var catId = 0;
    for (var i = 0, len = categories.length; i < len; ++i) {
        if (i == 0) {
            catId = categories[i].CATEGORY_ID;
        }
        createCategoriesButtons(categories[i].CATEGORY_NAME, categories[i].CATEGORY_ID);
    }
    createProductButtons(catId);
}
function createCategoriesButtons(value, id) {

    //Create an input type dynamically.   
    var element = document.createElement("input");
    //Assign different attributes to the element. 
    element.setAttribute("type", "button");
    element.setAttribute("value", value);
    element.setAttribute("name", value);
    element.setAttribute("id", id);
    element.setAttribute("class", "btn btn-large-categry btn-info");
    element.setAttribute("style", "margin: 2px 2px 2px 2px;");
    element.onclick = function () { // Note this is a function
        createProductButtons(this.id);
    };
    var dvLstCategory = document.getElementById("dv_lstCategory");
    //Append the element in page (in span).  
    dvLstCategory.appendChild(element);
}
//----------------------------------------------------------------------------------------------------
function loadAllProducts() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadAllProducts", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: sethfProductValue
            }
        );
}
function sethfProductValue(products) {

    products = JSON.stringify(products);
    var result = jQuery.parseJSON(products.replace(/&quot;/g, '"'));
    products = eval(result.d);
    products = JSON.stringify(products);
    document.getElementById("<%=hfProduct.ClientID%>").value = products;
    loadProductCategory();
}
function createProductButtons(catId) {
    var dvLstProducts = document.getElementById("div_lstProducts");
    while (dvLstProducts.hasChildNodes()) {
        dvLstProducts.removeChild(dvLstProducts.lastChild);
    }
    var lstProducts = document.getElementById("<%=hfProduct.ClientID%>").value;
    lstProducts = eval(lstProducts);
    for (var i = 0, len = lstProducts.length; i < len; ++i) {

        if (lstProducts[i].CATEGORY_ID == catId) {
            //Create an input type dynamically.   
            var element = document.createElement("input");
            //Assign different attributes to the element. 
            element.setAttribute("type", "button");
            element.setAttribute("value", lstProducts[i].SKU_NAME);
            element.setAttribute("name", lstProducts[i].SKU_NAME);
            element.setAttribute("id", lstProducts[i].SKU_ID);
            element.setAttribute("class", "btn btn-large-categry btn-info");
            element.setAttribute("style", "margin: 2px 2px 2px 2px;");
            element.onclick = function () { // Note this is a function
                addProductToOrderTable(this.id);
            };
            dvLstProducts.appendChild(element);

        }
    }
}
//-----------------------------------------------------------------------------------------------

function addProductToOrderTable(skuId) {
    var tableData;
    var lstProducts = document.getElementById("<%=hfProduct.ClientID%>").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        var flage = false;

        if (lstProducts[i].SKU_ID == skuId) {
            $('#tble-ordered-products').find('tr').each(function () {

                var td1 = $(this).find("td:eq(0)").text();
                if (skuId == td1) {
                    $(this).find("td:eq(2) input").val(parseInt($(this).find("td:eq(2) input").val()) + 1);
                    $(this).find("td:eq(4)").text(parseFloat($(this).find("td:eq(3)").text()) * parseInt($(this).find("td:eq(2) input").val()));
                    tableData = storeTblValues();
                    tableData = JSON.stringify(tableData);
                    document.getElementById('<%=hfOrderedproducts.ClientID%>').value = tableData;
                    flage = true;
                }
            });
            if (flage) break;
            addProductToOrderedProduct(lstProducts, i);
            break;
        }
    }
    tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('<%=hfOrderedproducts.ClientID%>').value = tableData;
    setTotals();
}

function addProductToOrderedProduct(lstProducts, i) {
    var rowcount = $("#tble-ordered-products tr").length - 1;

    var row;

    if (isOdd(rowcount)) {
        row = $('<tr ><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td align="left" class="pad-l-25">' + lstProducts[i].SKU_NAME + '</td><td align="center" ><a href="#"><img src="../images/btn-minus.jpg" /></a><input name="" type="text" class="t-item-2" placeholder="1" value="' + lstProducts[i].QTY + '" /><a href="#"><img src="../images/btn-plus.jpg" /></a></td><td>' + lstProducts[i].TRADE_PRICE + '</td><td>' + lstProducts[i].AMOUNT + '</td><td class="t-align-c"><a><img src="../images/delete.gif"  /></a></td><td style="display:none;">' + lstProducts[i].PRINTER_NAME + '</td><td style="display:none;">' + lstProducts[i].PRINT_COUNT + '</td><td style="display:none;">' + lstProducts[i].SECTION_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION_NAME + '</td><td style="display:none;">' + lstProducts[i].REMARKS + '</td><td style="display:none;">' + lstProducts[i].IS_VOID + '</td><td style="display:none;">' + lstProducts[i].CATEGORY_ID + '</td><td style="display:none;">' + lstProducts[i].SALE_INVOICE_MASTER_ID + '</td></tr>');
    } else {
        row = $('<tr  class="even" ><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td align="left" class="pad-l-25">' + lstProducts[i].SKU_NAME + '</td><td align="center" ><a href="#"><img src="../images/btn-minus.jpg" /></a><input name="" type="text" class="t-item-2" placeholder="1" value="' + lstProducts[i].QTY + '" /><a href="#"><img src="../images/btn-plus.jpg" /></a></td><td>' + lstProducts[i].TRADE_PRICE + '</td><td>' + lstProducts[i].AMOUNT + '</td><td class="t-align-c"><a><img src="../images/delete.gif"  /></a></td><td style="display:none;">' + lstProducts[i].PRINTER_NAME + '</td><td style="display:none;">' + lstProducts[i].PRINT_COUNT + '</td><td style="display:none;">' + lstProducts[i].SECTION_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION_NAME + '</td><td style="display:none;">' + lstProducts[i].REMARKS + '</td><td style="display:none;">' + lstProducts[i].IS_VOID + '</td><td style="display:none;">' + lstProducts[i].CATEGORY_ID + '</td><td style="display:none;">' + lstProducts[i].SALE_INVOICE_MASTER_ID + '</td></tr>');
    }
    $("#tble-ordered-products").append(row);
}
function isOdd(num) { return num % 2; }

function storeTblValues() {
    var tableData = new Array();
    $('#tble-ordered-products tr').each(function (row, tr) {

        tableData[row] = {
            "SKU_ID": $(tr).find('td:eq(0)').text(),
            "SKU_NAME": $(tr).find('td:eq(1)').text(),
            "QTY": $(tr).find('td:eq(2) input').val(),
            "TRADE_PRICE": $(tr).find('td:eq(3)').text(),
            "AMOUNT": $(tr).find('td:eq(4)').text(),
            //5 column is of delete button
            "PRINTER_NAME": $(tr).find('td:eq(6)').text(),
            "PRINT_COUNT": $(tr).find('td:eq(7)').text(),
            "SECTION_ID": $(tr).find('td:eq(8)').text(),
            "SECTION_NAME": $(tr).find('td:eq(9)').text(),
            "REMARKS": $(tr).find('td:eq(10)').text(),
            "IS_VOID": $(tr).find('td:eq(11)').text(),
            "CATEGORY_ID": $(tr).find('td:eq(12)').text(),
            "SALE_INVOICE_MASTER_ID": $(tr).find('td:eq(13)').text()
        }
    });
    return tableData;
}
function setTotals() {
    var subtotal = 0.0;
    var grandTotal;
    var salesTax = document.getElementById("<%=hfSalesTax.ClientID%>").value;
    $('#tble-ordered-products tr').each(function (row, tr) {
        subtotal += parseFloat($(tr).find("td:eq(4)").text());
    });
    salesTax = parseFloat(salesTax) / 100 * subtotal;
    grandTotal = salesTax + subtotal;
    document.getElementById("subTotal").innerHTML = subtotal;
    document.getElementById("SalesTax").innerHTML = salesTax;
    document.getElementById("GrandTotal").innerHTML = grandTotal;
}
/*--This JavaScript method execute when we click on Hold Order Button--*/
function HoldOrder() {
    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);
    var uniqueSections = $.unique(orderedProducts.map(function (d) { return d.SECTION_ID; }));

    for (var j = 0; j < uniqueSections.length; j++) {
        $('#detail-section-skus').empty(); // clear all skus  from invoice

        for (var i = 0, len = orderedProducts.length; i < len; i++) {
            if (orderedProducts[i].SECTION_ID == uniqueSections[j]) {
                $("#SectionName").text(orderedProducts[i].SECTION_NAME);
                $("#CustoerType").text($("#<%=hfCustomerType.ClientID%>").val());
                $("#TableNo").text($("#<%=hfTableNo.ClientID%>").val());
                $("#Date").text(moment().format('D-MMM-YYYY'));
                $("#Time").text(moment().format('hh:mm A'));
                var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td></tr>');
                $('#detail-section-skus').append(row);
            }
        }
        PrintInvoiceOfSection();
    }
    SaveOrderInDataBase();
}
function PrintInvoiceOfSection() {
    $.print("#invoice-kitchen");
}
function SaveOrderInDataBase() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/HoldOrder", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: "{'orderedProducts':'" + $('#hfOrderedproducts').val() + "'}",
                success: OrderSaved,
                error: OrderNotSaved
            }
        );
}
function OrderSaved() {
    $('#tble-ordered-products').empty();
    $('#<%=hfOrderedproducts.ClientID%>').val('');
    GetPendingBills();
}

function OrderNotSaved() {
    alert('error');
}

//--------------------------------------Pending Bills---------------------------------------------
function GetPendingBills() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/SelectPendingBills", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: LoadPendingBills
            }
        );
}
function LoadPendingBills(pendingBills) {

    pendingBills = JSON.stringify(pendingBills);
    pendingBills = jQuery.parseJSON(pendingBills.replace(/&quot;/g, '"'));
    pendingBills = eval(pendingBills.d);

    $("#tbl-pending-bills").empty();
    for (var i = 0, len = pendingBills.length; i < len; i++) {
        var rowcount = $("#tbl-pending-bills tr").length;
        var row;
        if (isOdd(rowcount)) {
            row = $('<tr ><td style="display:none;">' + pendingBills[i].SALE_INVOICE_ID + '</td><td align="left" class="pad-l-25" style="width:50%;">' + pendingBills[i].TABLE_NO + '</td><td style="width:50%;">' + pendingBills[i].AMOUNTDUE + '</td></tr>');
        } else {
            row = $('<tr class="even"><td style="display:none;">' + pendingBills[i].SALE_INVOICE_ID + '</td><td align="left" class="pad-l-25" style="width:50%;">' + pendingBills[i].TABLE_NO + '</td><td style="width:50%;">' + pendingBills[i].AMOUNTDUE + '</td></tr>');
        }
        $('#tbl-pending-bills').append(row);
    }
}
