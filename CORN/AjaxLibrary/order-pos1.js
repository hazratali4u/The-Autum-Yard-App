
function waitLoading(msg) {
    new $.Zebra_Dialog('<strong>' + msg + '</strong>', {
        'buttons': false,
        'position': ['top + 120'],
        'auto_close': 600
    });
}

function doPostbackTables() {
    if (document.getElementById("hfCustomerType").value == "Dine In") {
        __doPostBack('UPTables', '');
    }
}

$(document).ready(function () {

    window.onbeforeunload = preventMultipleSubmissions;

    waitLoading('Loading');

    $(".nav-ic img").click(function () {
        //e.preventDefault();
        $(".exit-text").slideToggle("slow", function () {
        });
    });

    MaxOrderNo();
    loadSaleForce();
    loadAllProducts();

    GetPendingBills();

    $("#tbl-pending-bills").delegate("tr", "click", function () {

        ShowBill(this);

    });

    //On Payments Click
    $("#Payments").click(function () {
      
        if ($('#hfServiceCharges').val() == "True") {//For Services Charges

            document.getElementById("lblGSTOrService").innerHTML = "Service Charges";
            document.getElementById("dvServiceCharges").setAttribute("style", "display:block;");
        }
        if ($('#hfDisable').val() == "1") {

            var tableData = storeTblValues();

            if (tableData.length > 0) {
               
                setTotals();
                $('#payment').show("slow");
                //$("#lblDiscountTotal2").text('');
            }
        }

    });

    //On Print Invoice CLick
    $("#PrintInvoice").click(function () {
        
        if ($('#hfServiceCharges').val() == "True") {//For Services Charges

            document.getElementById("lblGSTOrService2").innerHTML = "Service Charges";
            document.getElementById("dvServiceCharges2").setAttribute("style", "display:block;");

        }

        if ($('#hfDisable').val() == "1") {

            var tableData = storeTblValues();

            if (tableData.length > 0) {
               
                setTotals();

                $('#payment2').show("slow");
            }
        }
    });


});

//#region Service Type Call in ShowBill

function activeLink() {

    $("#lnkDineIn").removeClass("active");
    $("#lnkTakeaway").removeClass("active");
    $("#lnkDelivery").removeClass("active");

    if ($("#TableNo1").text() == "DLY") {
        $("#lnkDelivery").addClass("active");
        document.getElementById("hfCustomerType").value = "Delivery";

    } else if ($("#TableNo1").text() == "TKY") {

        $("#lnkTakeaway").addClass("active");
        document.getElementById("hfCustomerType").value = "Takeaway";

    } else {

        $("#lnkDineIn").addClass("active");
        document.getElementById("hfCustomerType").value = "Dine In";
    }
}

//Used in addProductToOrderTable(),LoadPendingBill(), On Payment Click, Print Invoice Click

function setTotals() {
    var subtotal = 0.0;
    var grandTotal;
    var salesTax = 0;// document.getElementById("hfSalesTax").value;

    if ($('#hfServiceCharges').val() == "False") {
        salesTax = document.getElementById("hfSalesTax").value;
    }
    else {
        salesTax = document.getElementById("txtService").value;
    }
    if (salesTax == "") {
        salesTax = 0;
    }

    var discountType = document.getElementById("hfDiscountType").value;
    var discount = document.getElementById('txtDiscount').value;
    var balance = 0;

    $('#tble-ordered-products tr').each(function (row, tr) {
        subtotal += parseFloat($(tr).find("td:eq(6)").text());
    });

    if (discount > 0) {
        if (discountType == 0) {

            grandTotal = subtotal;

            discount = grandTotal * (discount / 100);

            salesTax = parseFloat(salesTax) / 100 * (parseFloat(grandTotal) - parseFloat(discount));

            balance = Math.round((grandTotal - discount + salesTax), 0);

        }
        else if (discountType == 1) {

            grandTotal = subtotal;

            salesTax = parseFloat(salesTax) / 100 * (grandTotal - discount);

            balance = Math.round((grandTotal - discount + salesTax), 0);
        }
    }
    else {

        grandTotal = subtotal;

        salesTax = (parseFloat(salesTax) / 100) * subtotal;

        balance = Math.round((salesTax + subtotal), 0);
    }

    //----------Payment PopUp--------------\\
    document.getElementById("subTotal").innerHTML = Math.round(subtotal, 0);
    document.getElementById("lblGSTTotal").innerHTML = Math.round(salesTax, 0);
    document.getElementById("GrandTotal").innerHTML = Math.round(grandTotal, 0);
    document.getElementById("lblBalance").innerHTML = "0"; Math.round(0);
    document.getElementById("lblPaymentDue").innerHTML = Math.round(balance);

    //----------Print Invoice PopUp--------------\\
    document.getElementById("subTotal2").innerHTML = Math.round(subtotal, 0);
    document.getElementById("lblGSTTotal2").innerHTML = Math.round(salesTax, 0);
    document.getElementById("GrandTotal2").innerHTML = Math.round(grandTotal, 0);
    document.getElementById("lblPaymentDue2").innerHTML = Math.round(balance);


}

function MaxOrderNo() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadMaxOrderNo", //page/method name
                contentType: "application/json; charset=utf-8",
                success: GetMaxOrderNo,
            }
        );
}

function GetMaxOrderNo(data) {

    var result = eval(data.d);
    $("#MaxOrderNo").text(result);

}


//#region Sale Force
function loadSaleForce() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadSaleForce", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ customerType: document.getElementById("hfCustomerType").value }),
                success: addSaleForce


            }
        );
}
function addSaleForce(saleForce) {

    saleForce = JSON.stringify(saleForce);

    var result = jQuery.parseJSON(saleForce.replace(/&quot;/g, '"'));
    saleForce = eval(result.d);

    // var opt = document.createElement("option");

    var listItems = "";
    for (var i = 0; i < saleForce.length; i++) {
        listItems += "<option value='" + saleForce[i].USER_ID + "'>" + saleForce[i].USER_NAME + "</option>";
    }

    $("#ddlOrderBooker").html(listItems);
}
//#endregion Sale Force

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
        addProductToOrderedProduct(products, i, $("#OrderNo1").text());
    }
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    setTotals();
}

function loadAllProducts() {

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadAllProducts", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: sethfProductValue,
                error: OnError
            }
        );
}
function sethfProductValue(products) {

    products = JSON.stringify(products);

    var result = jQuery.parseJSON(products.replace(/&quot;/g, '"'));

    products = eval(result.d);
    products = JSON.stringify(products);
    document.getElementById("hfProduct").value = products;

    loadProductCategory();
}


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
    element.setAttribute("class", "box-sm");
    element.onclick = function () { // Note this is a function
        createProductButtons(this.id);
        changeClass(this);
    };
    var dvLstCategory = document.getElementById("dv_lstCategory");
    //Append the element in page (in span).  
    dvLstCategory.appendChild(element);
}
function createProductButtons(catId) {
    var dvLstProducts = document.getElementById("div_lstProducts");
    while (dvLstProducts.hasChildNodes()) {
        dvLstProducts.removeChild(dvLstProducts.lastChild);
    }
    var lstProducts = document.getElementById("hfProduct").value;
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
            element.setAttribute("class", "box-sm2");
            element.onclick = function () { // Note this is a function
                addProductToOrderTable(this.id);
                changesubClass(this)
            };
            dvLstProducts.appendChild(element);

        }
    }
}


//Products Grid---------------------------------------------------------------\\

function addProductToOrderTable(skuId) {
    var tableData;
    var lstProducts = document.getElementById("hfProduct").value;
    lstProducts = eval(lstProducts);

    for (var i = 0, len = lstProducts.length; i < len; ++i) {
        var flage = false;

        if (lstProducts[i].SKU_ID == skuId) {
            $('#tble-ordered-products').find('tr').each(function () {

                var td1 = $(this).find("td:eq(0)").text();
                if (skuId == td1) {

                    if ($(this).find("td:eq(19)").text() == "2") {//WHEN DIVISION IS VALUE BASE

                        $(this).find("td:eq(5) input").val(parseFloat($(this).find("td:eq(5) input").val()) + parseFloat($(this).find("td:eq(20)").text()));
                        $(this).find("td:eq(3) input").val(parseFloat($(this).find("td:eq(5) input").val()) / parseFloat($(this).find("td:eq(20)").text()));
                    }
                    else {
                        $(this).find("td:eq(3) input").val(parseFloat($(this).find("td:eq(3) input").val()) + 1);
                        $(this).find("td:eq(6)").text(parseFloat($(this).find("td:eq(5)").text()) * parseInt($(this).find("td:eq(3) input").val()));

                    }
                    tableData = storeTblValues();
                    tableData = JSON.stringify(tableData);
                    document.getElementById('hfOrderedproducts').value = tableData;
                    flage = true;
                }
            });
            if (flage) break;


            addProductToOrderedProduct(lstProducts, i, $("#OrderNo1").text());

            break;
        }
    }
    tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    setTotals();
}

function addProductToOrderedProduct(lstProducts, i, invoiceId) {
    var row = "";

    if (lstProducts[i].DIVISION_ID == "2") {//WHEN DIVISION IS VALUE BASE

        row = $('<tr ><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td align="center"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td><input type="text" size="4" onkeyup="plusAmount(this);" onkeypress="return onlyDotsAndNumbers(this, event);" value="' + lstProducts[i].TRADE_PRICE + '"></td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER_NAME + '</td><td style="display:none;">' + lstProducts[i].PRINT_COUNT + '</td><td style="display:none;">' + lstProducts[i].SECTION_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION_NAME + '</td><td style="display:none;">' + lstProducts[i].REMARKS + '</td><td style="display:none;">' + lstProducts[i].IS_VOID + '</td><td style="display:none;">' + lstProducts[i].CATEGORY_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESCRIPTION + '</td><td style="display:none;">' + lstProducts[i].IS_PRINT + '</td><td style="display:none;">' + lstProducts[i].DIVISION_ID + '</td><td style="display:none;">' + lstProducts[i].ACTUAL_PRICE + '</td></tr>');
    }
    else {
        row = $('<tr ><td style="display:none;">' + lstProducts[i].SKU_ID + '</td><td class="table-text2">' + lstProducts[i].SKU_NAME + '</td><td align="center" onclick="plusQty(this);"><span style="margin-right: 5px; cursor: pointer;"><img src="../images/plus.png"></span></td> <td align="center"><input type="text" size="2" value="' + lstProducts[i].QTY + '"  style="text-align: center;" disabled ></td><td align="center" onclick="decreaseItem(this);"><span style="margin-left: 5px; cursor: pointer;"><img src="../images/minus.png"></span></td><td class="table-text2">' + lstProducts[i].TRADE_PRICE + '</td><td class="table-text2">' + lstProducts[i].AMOUNT + '</td><td align="center" onclick="deleteItem(this);"><a href="#"><span class="fa fa-times"></span></a></td><td style="display:none;">' + lstProducts[i].PRINTER_NAME + '</td><td style="display:none;">' + lstProducts[i].PRINT_COUNT + '</td><td style="display:none;">' + lstProducts[i].SECTION_ID + '</td><td style="display:none;">' + lstProducts[i].SECTION_NAME + '</td><td style="display:none;">' + lstProducts[i].REMARKS + '</td><td style="display:none;">' + lstProducts[i].IS_VOID + '</td><td style="display:none;">' + lstProducts[i].CATEGORY_ID + '</td><td style="display:none;">' + invoiceId + '</td><td style="display:none;">' + lstProducts[i].IS_DESC + '</td><td style="display:none;">' + lstProducts[i].DESCRIPTION + '</td><td style="display:none;">' + lstProducts[i].IS_PRINT + '</td><td style="display:none;">' + lstProducts[i].DIVISION_ID + '</td></tr>');
    }
    $("#tble-ordered-products").append(row);
}

//WHEN DIVISION IS VALUE BASE
function plusAmount(obj) {

    rowIndex = obj.closest('tr');

    var price = parseFloat($(rowIndex).find('td:eq(5) input').val());

    if (obj.value != "") {

        var tprice = parseFloat($(rowIndex).find('td:eq(20)').text());

        $(rowIndex).find('td:eq(3) input').val(parseFloat(price / tprice).toFixed(2));

        var amount = parseFloat(price).toFixed(2);

        $(rowIndex).find('td:eq(6)').text(amount);

       

    }
    else {
        $(rowIndex).find('td:eq(3) input').val(0);
        $(rowIndex).find('td:eq(6)').text(0);
    }

    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    $('#hfDisable').val("0");
}

function plusQty(obj) {

    var rowIndex = $(obj).parent();
    var qty = parseFloat($(rowIndex).find('td:eq(3) input').val());
    qty = qty + 1;

    $(rowIndex).find('td:eq(3) input').val(qty);
    var price = $(rowIndex).find('td:eq(5)').text();
    var amount = parseFloat(qty * price).toFixed(2);
    $(rowIndex).find('td:eq(6)').text(amount);

    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;

    $('#hfDisable').val("0");
}

function minusQty() {
    var Sku_Id = $("#hfskuId").val();
    var qty = 1;
    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(0)').text() == Sku_Id) {
            qty = $(tr).find('td:eq(3) input').val();

            if (qty == "1") {

            } else {

                var nQty = qty - 1;
                $(tr).find('td:eq(3) input').val(nQty);
                var price = $(tr).find('td:eq(5)').text();
                var amount = parseFloat(nQty * price).toFixed(2);
                $(tr).find('td:eq(6)').text(amount);

            }
        }
    });
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    $("#hfskuId").val('');

}

//-----------------------------------------------------------------------------\\

function storeTblValues() {

    var tableData = new Array();

    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(19)').text() == "2") {
            
            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(3) input').val(),
                //4 for -
                "TRADE_PRICE": $(tr).find('td:eq(5) input').val(),
                "AMOUNT": $(tr).find('td:eq(6)').text(),
                //7 column is of delete button
                "PRINTER_NAME": $(tr).find('td:eq(8)').text(),
                "PRINT_COUNT": $(tr).find('td:eq(9)').text(),
                "SECTION_ID": $(tr).find('td:eq(10)').text(),
                "SECTION_NAME": $(tr).find('td:eq(11)').text(),
                "REMARKS": $(tr).find('td:eq(12)').text(),
                "IS_VOID": $(tr).find('td:eq(13)').text(),
                "CATEGORY_ID": $(tr).find('td:eq(14)').text(),
                "SALE_INVOICE_MASTER_ID": $(tr).find('td:eq(15)').text(),
                "IS_DESC": $(tr).find('td:eq(16)').text(),
                "DESCRIPTION": $(tr).find('td:eq(17)').text(),
                "IS_PRINT": $(tr).find('td:eq(18)').text()

            }
        }
        else {

            tableData[row] = {
                "SKU_ID": $(tr).find('td:eq(0)').text(),
                "SKU_NAME": $(tr).find('td:eq(1)').text(),
                //2 for +
                "QTY": $(tr).find('td:eq(3) input').val(),
                //4 for-
                "TRADE_PRICE": $(tr).find('td:eq(5)').text(),
                "AMOUNT": $(tr).find('td:eq(6)').text(),
                //7 column is of delete button
                "PRINTER_NAME": $(tr).find('td:eq(8)').text(),
                "PRINT_COUNT": $(tr).find('td:eq(9)').text(),
                "SECTION_ID": $(tr).find('td:eq(10)').text(),
                "SECTION_NAME": $(tr).find('td:eq(11)').text(),
                "REMARKS": $(tr).find('td:eq(12)').text(),
                "IS_VOID": $(tr).find('td:eq(13)').text(),
                "CATEGORY_ID": $(tr).find('td:eq(14)').text(),
                "SALE_INVOICE_MASTER_ID": $(tr).find('td:eq(15)').text(),
                "IS_DESC": $(tr).find('td:eq(16)').text(),
                "DESCRIPTION": $(tr).find('td:eq(17)').text(),
                "IS_PRINT": $(tr).find('td:eq(18)').text()

            }
        }
    });
    return tableData;
}


function GetPendingBills() {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectPendingBills", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ customerType: document.getElementById("hfCustomerType").value }),
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

        var row = $('<tr ><td style="display:none;">' + pendingBills[i].SALE_INVOICE_ID + '</td><td class="leftval">' + pendingBills[i].TABLE_NO + '</td><td class="rightval">' + pendingBills[i].AMOUNTDUE + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT + '</td><td style="display:none;">' + pendingBills[i].DISCOUNT_TYPE + '</td><td style="display:none;">' + pendingBills[i].coverTable + '</td><td style="display:none;">' + pendingBills[i].orderBookerId + '</td><td style="display:none;">' + pendingBills[i].approvedBy + '</td><td style="display:none;">' + pendingBills[i].SERVICE_CHARGES + '</td><td style="font-size: 12px; color: red;">' + diff(pendingBills[i].TIME_STAMP, document.getElementById('ct').innerHTML) + '</td><td style="display:none;">' + pendingBills[i].TABLE_NO2 + '</td><td style="display:none;">' + pendingBills[i].CUSTOMER_ID + '</td><td style="display:none;">' + pendingBills[i].CONTACT_NUMBER + '</td><td style="display:none;">' + pendingBills[i].ORDER_NO + '</td></tr>');

        $('#tbl-pending-bills').append(row);
    }


}

//Call on Pending Bills Row Click, And on HoldOrder Save When Takeaway
function ShowBill(tr) {

    $("#OrderNo1").text($(tr).find("td:eq(0)").text());
    $("#MaxOrderNo").text($(tr).find("td:eq(13)").text());
    $("#hfTableNo").val($(tr).find("td:eq(1)").text());

    var amountDue = $(tr).find("td:eq(2)").text();
    var discount = $(tr).find("td:eq(3)").text();

    $("#ddlOrderBooker").val($(tr).find("td:eq(6)").text());

    $("#TableNo1").text($(tr).find("td:eq(10)").text());

    $("#hfCustomerId").val($(tr).find("td:eq(11)").text());

    $("#InvoiceTable").text($(tr).find("td:eq(12)").text());

    $('#hfDiscountType').val($(tr).find("td:eq(4)").text());

    if ($(tr).find("td:eq(5)").text() != "null") {
        $('#txtCoverTable').val($(tr).find("td:eq(5)").text());
    }


    if ($(tr).find("td:eq(7)").text() != "null") {
        $('#txtApprovedBy2').val($(tr).find("td:eq(7)").text());
        $('#txtApprovedBy').val($(tr).find("td:eq(7)").text());
    }
    else {
        $('#txtApprovedBy2').val('');
        $('#txtApprovedBy').val('');
    }
    if ($(tr).find("td:eq(8)").text() != "0") {

        $('#txtService').val($(tr).find("td:eq(8)").text());
        $('#txtService2').val($(tr).find("td:eq(8)").text());
    }
    else {

        $('#txtService').val('');
        $('#txtService2').val('');
    }

    activeLink(); //Active css of Main Buttons on selection of pending bills

    if (amountDue > 0) {

        GetPendingBill($(tr).find("td:eq(0)").text());
        calculateDiscount(discount, amountDue);
    }
    else {//For Delivery Type
        $('#tble-ordered-products').empty();
        $('#hfOrderedproducts').val('');
    }

    $('#hfDisable').val("1");

}

function NewOrder() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');

    GetPendingBills();

    $("#hfTableNo").val("");
    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $("#txtCoverTable").val('');
    $("select#ddlOrderBooker").prop('selectedIndex', 0);

    MaxOrderNo();
    doPostbackTables();
}
//#region Printing

//For Removing Duplicates an Array Used in PrintOrder(), UniqueSection()
function unique(list) {
    var result = [];
    $.each(list, function (i, e) {
        if ($.inArray(e, result) == -1)
            result.push(e);
    });
    return result;
}

function PrintOrder() {

    if ($("#hfDisable").val() == "1") {
        var orderedProducts = $("#hfOrderedproducts").val();
        orderedProducts = eval(orderedProducts);
        var uniqueSections = $.unique(orderedProducts.map(function (d) { return d.SECTION_ID; }));

        uniqueSections = uniqueSections.sort();

        uniqueSections = unique(uniqueSections);


        for (var j = 0; j < uniqueSections.length; j++) {
            $('#detail-section-skus').empty(); // clear all skus  from invoice

            for (var i = 0, len = orderedProducts.length; i < len; i++) {
                if (orderedProducts[i].IS_PRINT == "true") {
                    if (orderedProducts[i].SECTION_ID == uniqueSections[j]) {

                        var qty = orderedProducts[i].QTY;
                        //if (qty > 0) {
                            $("#SectionName").text(orderedProducts[i].SECTION_NAME);
                            $("#CustoerType").text(document.getElementById("hfCustomerType").value);
                            $("#TableNo").text("Table No : "+ $("#hfTableNo").val());

                            $("#kitchenOrderTaker").text("O-T : " + $('select#ddlOrderBooker option:selected').text());

                            if ($("#hfCustomerType").val() == "Takeaway") {
                                $("#TableNo ").text("Customer : TKY");
                            }
                            else if ($("#hfCustomerType").val() == "Delivery") {
                                $("#TableNo").text("Customer :" + $("#hfTableNo").val());
                                $("#kitchenOrderTaker").text("D-M : " + $('select#ddlOrderBooker option:selected').text());
                            }

                            $("#Date").text($("#hfCurrentWorkDate").val());
                            $("#Time").text(moment().format('hh:mm A'));
                            $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());
                            var row = $('<tr ><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + qty + '</td></tr>');
                            $('#detail-section-skus').append(row);
                       // }
                    }
                }
            }
            if ($("#detail-section-skus tr").length > 0) {

                PrintInvoiceOfSection();

            }
        }
    }
    NewOrder();
}


function ShowHideInvoiceFootGst(Gst) {
    if (Gst == 0) {

        document.getElementById("GstRow").style.display = "none";
    }
    else {
        document.getElementById("GstRow").style.display = "table-row";
    }
}
function ShowHideInvoiceFootDiscount(Discount) {

    if (Discount == "false") {
        document.getElementById("DiscountRow").style.display = "none";

    }
    else {
        document.getElementById("DiscountRow").style.display = "table-row";

    }
}
function ShowHideInvoiceFootTotal(Gst, Discount) {

    if ((Gst == 0) && (Discount == 0)) {

        document.getElementById("GrandTotalRow").style.display = "none";
    }
    else {
        document.getElementById("GrandTotalRow").style.display = "table-row";
    }
}

function PrintSaleInvoice() {

    $("#InvoiceDate").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));
    $("#CustomerType").text(document.getElementById("hfCustomerType").value);

    if (document.getElementById("hfRegNo").value == "") {
        document.getElementById('RegNo').style.display = "none";
    }
    else {
        $('#RegNo').show();
    }
    if (document.getElementById("hfCustomerType").value == "Takeaway") {

        document.getElementById('OrderTakerName').style.display = "none";
        document.getElementById('OrderTaker').style.display = "none";
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
        // document.getElementById('CustomerNameDetail').style.display = "none";
        document.getElementById('CustomerDetail').style.display = "none";
        document.getElementById('InvoiceTableName').style.display = "none";
        document.getElementById('InvoiceTable').style.display = "none";
    }
    else if (document.getElementById("hfCustomerType").value == "Delivery") {

        $("#OrderTakerName").text("D-M:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        //   $("#CustomerNameDetail").text("Name & Addr:");

        var CDetail = $("#hfTableNo").val();
        $("#CustomerDetail").text(CDetail.substring(0, 42));

        $("#InvoiceTableName").text("Ph:");

        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        //  $('#CustomerNameDetail').show();
        $('#CustomerDetail').show();
        $('#InvoiceTableName').show();
        $('#InvoiceTable').show();

        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
    }
    else {


        $("#OrderTakerName").text("O-T:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        $("#CoverTableName").text("Cover Table:");
        $("#CoverTable").text($("#txtCoverTable").val());
        $("#InvoiceTableName").text("Table No:");
        $("#InvoiceTable").text($("#TableNo1").text());


        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        $('#CoverTableName').show();
        $('#CoverTable').show();
        $('#InvoiceTableName').show();
        $('#InvoiceTable').show();

        // document.getElementById('CustomerNameDetail').style.display = "none";
        document.getElementById('CustomerDetail').style.display = "none";

    }

    $("#BillNo").text($("#OrderNo1").text());

    //#region Products Detail

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);

    $('#invoiceDetailBody').empty(); // clear all skus  from invoice

    for (var i = 0, len = orderedProducts.length; i < len; i++) {
        var qty = orderedProducts[i].QTY;
        if (qty > 0) {
            if (orderedProducts[i].IS_DESC == "true") {

                var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '<br />' + orderedProducts[i].DESCRIPTION + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td><td class="text-right">' + orderedProducts[i].TRADE_PRICE + '</td><td class="text-right">' + orderedProducts[i].AMOUNT + '</td></tr>');
            }
            else {
                var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td><td class="text-right">' + orderedProducts[i].TRADE_PRICE + '</td><td class="text-right">' + orderedProducts[i].AMOUNT + '</td></tr>');
            }
            $('#invoiceDetailBody').append(row);
        }
    }

    //#endregion Products Detail

    var Gst = 0;

    if ($('#hfServiceCharges').val() == "False") {
        Gst = document.getElementById("hfSalesTax").value;
    }
    else {
        Gst = document.getElementById("txtService2").value;
        $("#GST-text").text("Service Chg @" + Gst + " % :");
    }
    if (Gst == "")
    { Gst = 0; }

    $("#TotalValue").text($("#subTotal").text());
    $("#GrandTotal-value").text($("#GrandTotal").text());

    var amountDue = $("#subTotal").text();

    var text = "";
    //When Discount not enter through print invoice POpUP
    if ($("#lblDiscountTotal2").text() == "0") {

        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);
        $("#Discount-text").text(text);
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#GrandTotal-value").text($("#lblPaymentDue2").text());
        $("#Discount-value").text(text);


        ShowHideInvoiceFootDiscount("false");
    }
    else {
        var discount = $("#lblDiscountTotal2").text();
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);

        if ($("#hfDiscountType").val() == "0") {

            var dic = "Discount @" + $("#txtDiscount2").val() + " % :";

            $("#Discount-text").text(dic);
        }
        else {
            $("#Discount-text").text('Discount :');
        }
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#Discount-value").text($("#lblDiscountTotal2").text());
        $("#GrandTotal-value").text($("#lblPaymentDue2").text());

        ShowHideInvoiceFootDiscount("true");
    }

    $("#Gst-value").text(Math.round(Gst, 0));

    ShowHideInvoiceFootGst(Gst);


    ShowHideInvoiceFootTotal(Gst, $("#lblDiscountTotal2").text());

    $("#PayIn-text").text(text);
    $("#PayIn-value").text(text);
    $("#balance-text").text(text);
    $("#balance-value").text(text);
    $("#balance-text2").text(text);
    $("#balance-value2").text(text);
    $("#PayType").text(text);

    $.print("#dvSaleInvoice");
}

//On Payment Save
function PrintPaymentInvoice() {

    $("#CustomerType").text(document.getElementById("hfCustomerType").value);
    $("#InvoiceDate").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    if (document.getElementById("hfRegNo").value == "") {
        document.getElementById('RegNo').style.display = "none";
    }
    else {
        $('#RegNo').show();
    }

    if (document.getElementById("hfCustomerType").value == "Takeaway") {

        document.getElementById('OrderTakerName').style.display = "none";
        document.getElementById('OrderTaker').style.display = "none";
        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
        //  document.getElementById('CustomerNameDetail').style.display = "none";
        document.getElementById('CustomerDetail').style.display = "none";
        document.getElementById('InvoiceTableName').style.display = "none";
        document.getElementById('InvoiceTable').style.display = "none";
    }
    else if (document.getElementById("hfCustomerType").value == "Delivery") {

        $("#OrderTakerName").text("D-M:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        //$("#CustomerNameDetail").text("Name & Addr:");

        var CDetail = $("#hfTableNo").val();
        $("#CustomerDetail").text(CDetail.substring(0, 32));

        $("#InvoiceTableName").text("Ph:");

        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        //  $('#CustomerNameDetail').show();
        $('#CustomerDetail').show();
        $('#InvoiceTableName').show();
        $('#InvoiceTable').show();

        document.getElementById('CoverTableName').style.display = "none";
        document.getElementById('CoverTable').style.display = "none";
    }
    else {


        $("#OrderTakerName").text("O-T:");
        var OTaker = $("#ddlOrderBooker option:selected").text();
        $("#OrderTaker").text(OTaker.substring(0, 10));

        $("#CoverTableName").text("Cover Table:");
        $("#CoverTable").text($("#txtCoverTable").val());
        $("#InvoiceTableName").text("Table No:");
        $("#InvoiceTable").text($("#TableNo1").text());


        $('#OrderTakerName').show();
        $('#OrderTaker').show();
        $('#CoverTableName').show();
        $('#CoverTable').show();
        $('#InvoiceTableName').show();
        $('#InvoiceTable').show();

        //document.getElementById('CustomerNameDetail').style.display = "none";
        document.getElementById('CustomerDetail').style.display = "none";

    }


    $("#BillNo").text($("#OrderNo1").text());

    //#region Products Detail

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);
    $('#invoiceDetailBody').empty(); // clear all skus  from invoice
    for (var i = 0, len = orderedProducts.length; i < len; i++) {
        var qty = orderedProducts[i].QTY;
        if (qty > 0) {
            if (orderedProducts[i].IS_DESC == "true") {

                var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '<br />' + orderedProducts[i].DESCRIPTION + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td><td class="text-right">' + orderedProducts[i].TRADE_PRICE + '</td><td class="text-right">' + orderedProducts[i].AMOUNT + '</td></tr>');
            }
            else {
                var row = $(' <tr><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td><td class="text-right">' + orderedProducts[i].TRADE_PRICE + '</td><td class="text-right">' + orderedProducts[i].AMOUNT + '</td></tr>');
            }

            $('#invoiceDetailBody').append(row);
        }
    }

    //#endregion Products Detail

    var Gst = 0;

    if ($('#hfServiceCharges').val() == "False") {
        Gst = document.getElementById("hfSalesTax").value;
    }
    else {
        Gst = document.getElementById("txtService").value;
        $("#GST-text").text("Service Chg @" + Gst + " % :");
    }
    if (Gst == "")
    { Gst = 0; }

    $("#TotalValue").text($("#subTotal").text());
    $("#GrandTotal-value").text($("#GrandTotal").text());

    var amountDue = $("#subTotal").text();

    var text = "";

    //When Discount not enter through Print invoice POpUP-----------------
    if ($("#lblDiscountTotal").text() == "0") {

        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue);
        $("#Discount-text").text(text);
        $("#Gst-value").text(Math.round(Gst, 0));
        $("#GrandTotal-value").text($("#lblPaymentDue").text());
        $("#Discount-value").text(text);

        ShowHideInvoiceFootDiscount("false");
    }
    else {
        var discount = $("#lblDiscountTotal").text();
        Gst = parseFloat(Gst) / 100 * parseFloat(amountDue - discount);

        if ($("#hfDiscountType").val() == "0") {

            var dic = "Discount @" + $("#txtDiscount").val() + " % :";
            $("#Discount-text").text(dic);
        }
        else {
            $("#Discount-text").text('Discount :');
        }

        $("#Gst-value").text(Math.round(Gst, 0));
        $("#Discount-value").text($("#lblDiscountTotal").text());
        $("#GrandTotal-value").text($("#lblPaymentDue").text());

        ShowHideInvoiceFootDiscount("true");
    }
    if ($('#hfPaymentType').val() == "1") {//For Credit Card Payment

        $("#PayIn-text").text('Payment IN :');

        var cash = $("#txtCashRecieved").val();
        var credit = $("#lblPaymentDue").text();
        var credit = credit - cash;

        if (cash > 0) {
            $("#PayType").text('MOP: Mixed');

            $("#PayIn-value").text($("#txtCashRecieved").val());

            $("#balance-text").text('Credit Card:');
            $("#balance-value").text(credit)

            $("#balance-text2").text('Balance :');
            $("#balance-value2").text(0);

        }
        else {
            $("#PayType").text('MOP: Credit Card');

            $("#PayIn-value").text(credit);

            $("#balance-text").text('Balance :');
            $("#balance-value").text(0)

            $("#balance-text2").text(text);
            $("#balance-value2").text(text);
        }

    }
    else {//For Cash Payment


        $("#PayType").text('MOP: Cash');

        $("#PayIn-text").text('Payment IN :');
        $("#balance-text").text('Change :');
        $("#balance-text2").text(text);

        $("#PayIn-value").text($("#txtCashRecieved").val());
        $("#balance-value").text($("#lblBalance").text());
        $("#balance-value2").text(text);
    }

    ShowHideInvoiceFootGst(Gst);
    ShowHideInvoiceFootTotal(Gst, $("#lblDiscountTotal").text());

    $.print("#dvSaleInvoice");
}

//#endregion Printing Section 

function preventMultipleSubmissions() {
    document.getElementById("#dvHold").disabled = true;
}

//function checkTable()
//{
//    $('#tble-ordered-products tr').each(function (row, tr) {
//        if ($(tr).find('td:eq(19)').text() == "2") {
//            if ($(tr).find('td:eq(5) input').val() == "") {
//                  return -1;
//            }
//        }
//    });
//    return 0;
//}
// #region HoldOrder
function HoldOrder() {



    //When Dine IN Active
    if (($("#lnkDineIn").attr("class")) == "box active") {

        if (($("#hfTableNo").val() == "") || ($("#hfTableNo").val() == "DLY") || ($("#hfTableNo").val() == "TKY")) {

            Error("Table not selected");

            return;
        }


        if ($("#hfIsCoverTable").val() == "1") {

            if (($("#txtCoverTable").val() == "0") || ($("#txtCoverTable").val() == "")) {

                Error("Cover Table not entered");

                return;
            }
        }
    }
    if (($("#lnkDelivery").attr("class")) == "box active") {

        if (($("#hfCustomerId").val() == "0")) {

            Error("Please select customer");

            document.getElementById('divCustomer').style.visibility = 'visible';
            return;
        }

    }


    SaveOrder();
}

function UniqueSection() {

    var orderedProducts = $("#hfOrderedproducts").val();
    orderedProducts = eval(orderedProducts);

    var uniqueSections = $.unique(orderedProducts.map(function (d) { return d.SECTION_ID; }));

    uniqueSections = uniqueSections.sort();
    uniqueSections = unique(uniqueSections);

    for (var j = 0; j < uniqueSections.length; j++) {
        $('#detail-section-skus').empty(); // clear all skus  from invoice

        for (var i = 0, len = orderedProducts.length; i < len; i++) {
            if (orderedProducts[i].IS_PRINT == "true") {
                if (orderedProducts[i].SECTION_ID == uniqueSections[j]) {

                    var qty = orderedProducts[i].QTY - orderedProducts[i].PRINT_COUNT;

                    var IS_VOID = orderedProducts[i].IS_VOID;

                    if (IS_VOID == "false") {
                        if (qty > 0) {

                            $("#SectionName").text(orderedProducts[i].SECTION_NAME);
                            $("#CustoerType").text($("#hfCustomerType").val());
                           

                            $("#TableNo").text("Table No : " + $("#hfTableNo").val());

                            $("#kitchenOrderTaker").text("O-T : " + $('select#ddlOrderBooker option:selected').text());

                            if ($("#hfCustomerType").val() == "Takeaway") {
                                $("#TableNo ").text("Customer : TKY");
                            }
                            else if ($("#hfCustomerType").val() == "Delivery") {
                                $("#TableNo").text("Customer :" + $("#hfTableNo").val());
                                $("#kitchenOrderTaker").text("D-M :" + $('select#ddlOrderBooker option:selected').text());
                            }

                            $("#Date").text($("#hfCurrentWorkDate").val());
                            $("#Time").text(moment().format('hh:mm A'));
                            $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());

                            var row = $('<tr ><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + qty + '</td></tr>');
                            $('#detail-section-skus').append(row);
                        }

                        if (orderedProducts[i].QTY == 0) {
                            if (qty == 0) {

                                $("#SectionName").text(orderedProducts[i].SECTION_NAME);
                                $("#CustoerType").text($("#hfCustomerType").val());
                                $("#TableNo").text("Table No : " + $("#hfTableNo").val());

                                $("#kitchenOrderTaker").text("O-T : " + $('select#ddlOrderBooker option:selected').text());

                                if ($("#hfCustomerType").val() == "Takeaway") {
                                    $("#TableNo ").text("Customer : TKY");
                                }
                                else if ($("#hfCustomerType").val() == "Delivery") {
                                    $("#TableNo ").text("Customer :" + $("#hfTableNo").val());
                                    $("#kitchenOrderTaker").text("D-M : " + $('select#ddlOrderBooker option:selected').text());
                                }

                                $("#Date").text($("#hfCurrentWorkDate").val());
                                $("#Time").text(moment().format('hh:mm A'));
                                $("#PrintMaxOrderNo").text($("#MaxOrderNo").text());

                                var row = $('<tr ><td>' + orderedProducts[i].SKU_NAME + '</td><td class="text-right">' + orderedProducts[i].QTY + '</td></tr>');
                                $('#detail-section-skus').append(row);
                            }
                        }
                    }
                }
            }
        }
        if ($("#detail-section-skus tr").length > 0) {

            PrintInvoiceOfSection();
        }
    }
}

function PrintInvoiceOfSection() {


    $("#hfDisable").val("0");

    $.print("#invoice-kitchen");
}

function SaveOrder() {

    var Id = $('select#ddlOrderBooker option:selected').val();

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/HoldOrder", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ orderedProducts: $('#hfOrderedproducts').val(), orderBooker: Id, coverTable: $('#txtCoverTable').val(), customerType: document.getElementById("hfCustomerType").value, CustomerName: document.getElementById("hfCustomerId").value, maxOrderNo: $("#MaxOrderNo").text(), printType: document.getElementById("hfBookingType").value, tableName: $("#hfTableNo").val(), bookerName: $('select#ddlOrderBooker option:selected').text() }),
                success: OrderSaved,
                error: OnError

            }
        );
}

function ClearOrder() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $('#hfCustomerId').val('0');

    GetPendingBills();

    $("#hfTableNo").val("");
    $('#hfDisable').val("0");

    $("#txtCoverTable").val('');

    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $("select#ddlOrderBooker").prop('selectedIndex', 0);

    MaxOrderNo();

    doPostbackTables();

}

function OrderSaved() {

    if (document.getElementById("hfBookingType").value == "0") {//For Kitchen dialog not print
        UniqueSection();
    }
    ClearOrder();

    if (document.getElementById("hfCustomerType").value == "Takeaway") {

        LoadLastBill();

        if ($('#hfServiceCharges').val() == "True") {//For Services Charges

            document.getElementById("lblGSTOrService").innerHTML = "Service Charges";
            document.getElementById("dvServiceCharges").setAttribute("style", "display:block;");
        }

        isDisable('false');

        $('#payment').show("slow");
    }

}

function Error(Msg) {

    $.Zebra_Dialog(Msg, { 'title': 'Error', 'type': 'error' });
}

function OnError(xhr, errorType, exception) {

    var responseText;
    
    try {
        responseText = jQuery.parseJSON(xhr.responseText);
        $.Zebra_Dialog(responseText.Message, { 'title': 'Error', 'type': 'error' });

    } catch (e) {
        responseText = xhr.responseText;
        $.Zebra_Dialog(responseText, { 'title': 'Error', 'type': 'error' });
    }
    
}
// #endregion HoldOrder

//----------------on Payment Type  button Click---------------------------------\\
function PayType(myPayType) {
    document.getElementById("cash").style["background-color"] = "#919399";
    document.getElementById("credit").style["background-color"] = "#919399";

    myPayType.style["background-color"] = "#7dab49";

    var currentValue = myPayType.value;
    $('#hfPaymentType').val(currentValue);
    CalculateBalance();

}

//----------------on Discount Type button Click---------------------------------\\
function DiscType(myDiscType) {

    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";

    //---on print invoice popup
    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";
    //-----------------------------------------------------------------------\\

    myDiscType.style["background-color"] = "#7dab49";
    var currentValue = myDiscType.value;

    // document.getElementById('txtDiscount').disabled = false;
    // document.getElementById('txtApprovedBy').disabled = false;

    document.getElementById('txtDiscount2').disabled = false;
    document.getElementById('txtApprovedBy2').disabled = false;

    $('#hfDiscountType').val(currentValue);

    if (myDiscType.id == 'percentage2' || myDiscType.id == 'value2') {

        CalculateBalance2();
    }
    else {

        CalculateBalance();
    }

}


//--------------------Calcualte on discount and cash Recd on Payment Click PopUp----\\ 

function CalculateBalance() {

    var balance = 0;
    var amountDue = 0;
    var discount = 0;
    var gst = 0;

    discount = document.getElementById('txtDiscount').value;

    var discountType = $('#hfDiscountType').val(); //  document.getElementById("hfDiscountType").value;
    var cashRcd = 0;

    cashRcd = document.getElementById('txtCashRecieved').value;

    var grandTotal = 0;
    grandTotal = $("#GrandTotal").text();


    if ($('#hfServiceCharges').val() == "False") {
        gst = document.getElementById("hfSalesTax").value;
    }
    else {
        gst = document.getElementById("txtService").value;
    }

    $("#lblBalance").text('');
    $("#lblPaymentDue").text('');

    if ((discount == "")) {
        discount = 0;
    }
    if ((cashRcd == "")) {
        cashRcd = 0;
    }
    if ((gst == "")) {
        gst = 0;
    }

    if (discountType == 0) {

        discount = parseFloat(grandTotal) * parseFloat(discount / 100);

        gst = parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount));

        // balance = parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount);

        balance = Math.round((grandTotal - discount + gst), 0);

        amountDue = Math.round(balance, 0);

        balance = cashRcd - balance;


    }
    else if (discountType == 1) {
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount));
        balance = parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount);
        amountDue = Math.round(balance, 0);

        balance = Math.round((cashRcd - balance), 0);
    }
    else {
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal - discount));

        balance = parseFloat(grandTotal) + parseFloat(gst);

        amountDue = parseFloat(balance) - parseFloat(discount);

        balance = Math.round((cashRcd - amountDue), 0);
    }

    if (cashRcd == 0) {
        balance = 0;
    }

    $("#lblDiscountTotal").text(parseFloat(discount).toFixed(0));
    $("#lblGSTTotal").text(parseFloat(gst).toFixed(0));
    $("#lblBalance").text(parseFloat(balance).toFixed(0));
    $("#lblPaymentDue").text(parseFloat(amountDue).toFixed(0));

}

function SaveInvoice() {

    var discType = 0;

    if ($('#hfDiscountType').val() != '') {
        discType = $('#hfDiscountType').val();
    }

    if ($('#hfPaymentType').val() == '') {

        Error("First Select Payment Type");

        return;

    }
    var salesTax = document.getElementById("hfSalesTax").value;


    $.ajax
    (
        {
            type: "POST", //HTTP method
            url: "frmOrderPOS.aspx/InsertInvoice", //page/method name
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ orderedProducts: $('#hfOrderedproducts').val(), amountDue: $("#GrandTotal").text(), discount: $('#txtDiscount').val(), paidIn: $('#txtCashRecieved').val(), payType: $('#hfPaymentType').val(), Gst: $("#lblGSTTotal").text(), DiscType: discType, approvedBy: $('#txtApprovedBy').val(), gstPerAge: salesTax, Service: $('#txtService').val() }),
            success: InvoiceSaved,
            error: OnError
        }
    );
}

function InvoiceSaved() {

    PrintPaymentInvoice();

    Clear();

    GetPendingBills();

    $("#hfTableNo").val("");

    MaxOrderNo();

    doPostbackTables();

}


function Clear() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');
    $('#hfPaymentType').val('');
    $('#txtDiscount').val('');
    $('#txtCashRecieved').val('');
    $('#txtApprovedBy').val('');
    $('#txtApprovedBy2').val('');
    $("#lblDiscountTotal").text('0.00');

    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#payment').hide("slow");

    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $('#hfCustomerId').val('0');
    $("#txtCoverTable").val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $("select#ddlOrderBooker").prop('selectedIndex', 0);
    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";
    document.getElementById("cash").style["background-color"] = "#919399";
    document.getElementById("credit").style["background-color"] = "#919399";
    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;


}

function ClearOnCancel() {
    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');

    $('#hfPaymentType').val('');
    $('#txtDiscount').val('');
    $('#txtCashRecieved').val('');
    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#txtApprovedBy').val('');
    $('#txtApprovedBy2').val('');
    $('#payment').hide("slow");
    $("#hfTableNo").val("");
    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $('#hfCustomerId').val('0');
    $("#txtCoverTable").val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $("select#ddlOrderBooker").prop('selectedIndex', 0);
    document.getElementById("percentage").style["background-color"] = "#919399";
    document.getElementById("value").style["background-color"] = "#919399";
    document.getElementById("cash").style["background-color"] = "#919399";
    document.getElementById("credit").style["background-color"] = "#919399";
    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;

    MaxOrderNo();
}

function changeClass(btn) {
    var a, n;
    var a = document.getElementById("dv_lstCategory").children;
    for (n = 0; n < a.length; n++) {
        a[n].style["background-color"] = "#d4def7";
    }
    btn.style["background-color"] = "#7DAB49";

}

function changesubClass(btn) {
    var a, n;
    var a = document.getElementById("div_lstProducts").children;
    for (n = 0; n < a.length; n++) {
        a[n].style["background-color"] = "#e2e3e8";
    }
    btn.style["background-color"] = "#7DAB49";

}

// #region Item Deletion

function UserValidationInDataBase(obj) {


    var UserId = document.getElementById('txtUserID').value;
    var UserPassword = document.getElementById('txtUserPass').value;
    var UserClick = document.getElementById('hfUserClick').value;

    //UserPassword.setAttribute('type', 'text');

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/ValidateUser", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ UserId: UserId, UserPass: UserPassword, UserClick: UserClick }),
                success: UserValidated,
                error: function () { Error("Authorization Failed!"); }
            }
        );
}

function UserValidated(UserAuth) {

    UserAuth = JSON.stringify(UserAuth);
    var result = jQuery.parseJSON(UserAuth.replace(/&quot;/g, '"'));
    UserAuth = eval(result.d);

    document.getElementById('txtUserID').value = "";
    document.getElementById('txtUserPass').value = "";


    $('#UserValidation').hide("slow");
    if ($("#hfUserClick").val() == "Decrease") {
        $('#DecreaseConfirmation').show("slow");
    }
    else {
        $('#DeletionConfirmation').show("slow");
    }
}

function CancelUserValidation() {

    $('#UserValidation').hide("slow");
}


function deleteItem(btn) {

    var rowIndex = $(btn).parent();
    var Sku_id = $(rowIndex).find('td:eq(0)').text();
    var SkuType = $(rowIndex).find('td:eq(12)').text();


    $("#hfskuId").val(Sku_id);

    if (SkuType == "") {
        deleteQty();
    }
    else {
        document.getElementById('txtUserID').value = "";
        document.getElementById('txtUserPass').value = "";

        $("#hfUserClick").val('Delete');
        $('#UserValidation').show("slow");

        $('#txtUserID').focus();
    }

}
function deleteQty() {
    var Sku_Id = $("#hfskuId").val();
    $('#tble-ordered-products tr').each(function (row, tr) {
        if ($(tr).find('td:eq(0)').text() == Sku_Id) {
            $(tr).find('td:eq(13)').text(true);
            $(tr).find('td:eq(7)').css('background-color', '#b20505');

        }
    });
    var tableData = storeTblValues();
    tableData = JSON.stringify(tableData);
    document.getElementById('hfOrderedproducts').value = tableData;
    $("#hfskuId").val('');
}


function DeletionProceed() {
    $('#DeletionConfirmation').hide("slow");
    deleteQty();
    $('#hfDisable').val("0");
}

function DeletionCancel() {
    $('#DeletionConfirmation').hide("slow");
    $("#hfskuId").val('');
    $("#hfUserClick").val('');

}

// #endregion Item Deletion


// #region Decrease Qty

function decreaseItem(btn) {

    var rowIndex = $(btn).parent();
    var Sku_id = $(rowIndex).find('td:eq(0)').text();
    var SkuType = $(rowIndex).find('td:eq(12)').text();
    var qty = $(rowIndex).find('td:eq(3) input').val();



    if (qty > 1) {
        $("#hfskuId").val(Sku_id);
        if (SkuType == "") {
            minusQty();
        }
        else {
            document.getElementById('txtUserID').value = "";
            document.getElementById('txtUserPass').value = "";
            $("#hfUserClick").val('Decrease');
            $('#UserValidation').show("slow");
            $('#txtUserID').focus();
        }
    }
}

function DecreaseProceed() {
    $('#DecreaseConfirmation').hide("slow");

    minusQty();
    $('#hfDisable').val("0");
}

function DecreaseCancel() {
    $('#DecreaseConfirmation').hide("slow");
    $("#hfskuId").val('');
    $("#hfUserClick").val('');

}

// #endregion Decrease Qty

//------------------------on Print INvoice Click ---------------\\

//-------get Discount,Discount Type from dataBase on Bill selectoin and set on Print Invoice,Payment PopUp--\\

function isDisable(flag) {
    if (flag == 'true') {


        document.getElementById("percentage").disabled = true;
        document.getElementById("value").disabled = true;
    }
    else {

        if (document.getElementById("hfCustomerType").value == "Takeaway") {

            document.getElementById("percentage").disabled = false;
            document.getElementById("value").disabled = false;

            document.getElementById('txtDiscount').disabled = false;
            document.getElementById('txtApprovedBy').disabled = false;
        }
    }
}

function isColored(flag) {
    if (flag == 'true') {

        document.getElementById("percentage2").style["background-color"] = "#7dab49";
        document.getElementById("percentage").style["background-color"] = "#7dab49";

        document.getElementById("value2").style["background-color"] = "#919399";
        document.getElementById("value").style["background-color"] = "#919399";
    }
    else {
        document.getElementById("value2").style["background-color"] = "#7dab49";
        document.getElementById("value").style["background-color"] = "#7dab49";

        document.getElementById("percentage2").style["background-color"] = "#919399";
        document.getElementById("percentage").style["background-color"] = "#919399";
    }
}
function calculateDiscount(discount, amountDue) {

    $('#lblDiscountTotal').text('0');
    $("#lblDiscountTotal2").text('0');

    document.getElementById('txtDiscount').value = "";
    document.getElementById('txtDiscount2').value = "";

    var salesTax = 0;
    if ($('#hfServiceCharges').val() == "False") {
        salesTax = document.getElementById("hfSalesTax").value;
        salesTax = parseFloat(salesTax) / 100 * amountDue;
    }
    else {
        salesTax = document.getElementById("txtService2").value;
    }
    if (salesTax == "") {
        salesTax = 0;
    }
    var Total = Math.round(parseFloat(amountDue), 2);

    if (discount > 0) {

        document.getElementById('txtDiscount').value = discount;
        document.getElementById('txtDiscount2').value = discount;


        if ($('#hfDiscountType').val() == 0) {

            discount = Total * (discount / 100);

            $("#lblDiscountTotal2").text(Math.round(discount, 0));
            $("#lblDiscountTotal").text(Math.round(discount, 0));


            isColored('true');

        }
        else {

            $("#lblDiscountTotal2").text(Math.round(discount, 0));
            $("#lblDiscountTotal").text(Math.round(discount, 0));

            isColored('false');

        }
        document.getElementById('txtDiscount2').disabled = true;

        if (document.getElementById("hfCustomerType").value == "Takeaway") {

            isDisable('false');
        }
        else {

            isDisable('true');
        }
        document.getElementById("lblGSTTotal").innerHTML = Math.round(salesTax);
        document.getElementById("lblGSTTotal2").innerHTML = Math.round(salesTax);
    }
    else {
        isDisable(false);
    }
}

//--------------------Calcualte on discount  on Print Invoice Click PopUp----\\ 
function CalculateBalance2() {
    var balance = 0;
    var amountDue = 0;
    var discount = 0;

    discount = document.getElementById('txtDiscount2').value;

    var discountType = $('#hfDiscountType').val();
    var grandTotal = $("#GrandTotal2").text();

    var gst = 0;
    if ($('#hfServiceCharges').val() == "False") {
        gst = document.getElementById("hfSalesTax").value;
    }
    else {
        gst = document.getElementById("txtService2").value;
    }

    $("#lblBalance").text('');
    $("#lblPaymentDue").text('');

    if (discount == "") {
        discount = 0;
    }

    if (gst == "") {
        gst = 0;
    }


    if (discountType == 0) {

        discount = parseFloat(grandTotal) * parseFloat(discount) / 100;
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal) - parseFloat(discount));
        amountDue = parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount);
        amountDue = Math.round(amountDue, 0);
    }
    else {
        gst = parseFloat(gst / 100) * (parseFloat(grandTotal - discount));
        amountDue = parseFloat(grandTotal) + parseFloat(gst) - parseFloat(discount);
        amountDue = Math.round(amountDue, 0);
    }

    $("#lblPaymentDue2").text(Math.round(amountDue, 0));
    $("#lblDiscountTotal2").text(Math.round(discount, 0));
    $("#lblGSTTotal2").text(Math.round(gst, 0));
}

function UpdateOrder() {

    var discType = 0;

    if ($('#hfDiscountType').val() != '') {
        discType = $('#hfDiscountType').val();
    }

    var salesTax = document.getElementById("hfSalesTax").value;

    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/UpdateOrder", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ orderedProducts: $('#hfOrderedproducts').val(), amountDue: $("#GrandTotal2").text(), discount: $('#txtDiscount2').val(), Gst: $("#lblGSTTotal2").text(), DiscType: discType, approvedBy: $('#txtApprovedBy2').val(), gstPerAge: salesTax, Service: $('#txtService2').val() }),
                success: invoiceSaved2,
                error: OnError
            }
        );
}

function invoiceSaved2() {

    PrintSaleInvoice();

    Clear2();

    GetPendingBills();

    $("#hfTableNo").val("");
    MaxOrderNo();

    doPostbackTables();
}
function Clear2() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');

    $('#txtDiscount2').val('');
    $('#txtApprovedBy2').val('');
    $('#txtApprovedBy').val('');

    $('#hfDiscountType').val('');
    $('#hfDisable').val("0");
    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $("#txtCoverTable").val('');
    $("#txtService").val('');
    $("#txtService2").val('');
    $('#hfCustomerId').val('0');
    $("select#ddlOrderBooker").prop('selectedIndex', 0);
    $('#payment2').hide("slow");

    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";

    document.getElementById('txtDiscount2').disabled = true;
    document.getElementById('txtApprovedBy2').disabled = true;

    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;
}
function ClearOnCancel2() {

    $('#tble-ordered-products').empty();
    $('#hfOrderedproducts').val('');

    $('#txtDiscount2').val('');
    $('#txtDiscount').val('');

    $('#hfDisable').val("0");
    $('#hfDiscountType').val('');
    $('#txtApprovedBy2').val('');
    $('#txtApprovedBy').val('');

    $("#TableNo1").text("N/A");
    $("#OrderNo1").text("N/A");
    $('#hfCustomerId').val('0');
    $("#txtCoverTable").val('');

    $("#txtService").val('');
    $("#txtService2").val('');

    $("select#ddlOrderBooker").prop('selectedIndex', 0);

    $('#payment2').hide("slow");

    document.getElementById("percentage2").style["background-color"] = "#919399";
    document.getElementById("value2").style["background-color"] = "#919399";

    document.getElementById('txtDiscount2').disabled = true;
    document.getElementById('txtApprovedBy2').disabled = true;

    document.getElementById('txtDiscount').disabled = true;
    document.getElementById('txtApprovedBy').disabled = true;

    MaxOrderNo();
}

//-------------------Sales Reports--------------------------------------------\\

function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

$('#btnShowReports').click(function (e) {

    if (document.getElementById('hfReport').value != "") {
        $('#reports-buttons').lightbox_me({
            centered: true,
            onLoad: function () {
                $('#reports-buttons').find('input:first').focus();
            }
        });

        var obj = jQuery.parseJSON(document.getElementById('hfReport').value);

        for (var i = 0; i < obj.length; i++) {
            var item = obj[i];
            $("#reports-buttons").find("#" + item.MODULE_KEY).css("display", "block");
        }

    }
    else {
        waitLoading('No report assign');

    }

    e.preventDefault();
});

$('#sales-summary').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectSalesSummary", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowSalesSummary
                }
            );

});

function ShowSalesSummary(data) {

    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);
    $("#lblAddress").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    $("#g-sale").text(": " + numberWithCommas(parseFloat(data[0].GROSS_AMOUNT).toFixed(2)));

    if ($('#hfServiceCharges').val() == "False") {


        $("#gst-text").text("G.S.T");
    }
    else {

        $("#gst-text").text("Service Charges");
    }
    $("#gst-amount").text(": " + numberWithCommas(parseFloat(data[0].GST_AMOUNT).toFixed(2)));

    $("#totalAmount").text(": " + numberWithCommas(data[0].TOTAL_AMOUNT));
    $("#discount-amount").text(": " + numberWithCommas(parseFloat(data[0].DISCOUNT).toFixed(2)));
    $("#net-amount").text(": " + numberWithCommas(parseFloat(data[0].NET_TOTAL).toFixed(2)));
    $("#cash-sales").text(": " + numberWithCommas(parseFloat(data[0].CashSales).toFixed(2)));
    $("#credit-card-sales").text(": " + numberWithCommas(parseFloat(data[0].CreditCardSales).toFixed(2)));
    // $("#cash-in-hand").text(numberWithCommas(Math.round(data[0].CashInHand), 2));
    $("#cash-in-hand").text(": " + numberWithCommas(parseFloat(data[0].CashInHand).toFixed(2)));

    $("#location-name-sales-summary").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-sales-summary").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    $.print("#sales-summary-report");
}

$('#sales-detail').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectSalesDetail", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowSalesDetail
                }
            );
});

function ShowSalesDetail(data) {
    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);
    $("#lblAddress2").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    $('#sales-detail-data').empty(); // clear all skus  from invoice
    var grandTotal = 0;
    for (var i = 0, len = data.length; i < len; i++) {
        var row = $(' <tr><td>' + data[i].TableDefination_No + '</td><td class="text-right">' + numberWithCommas(parseFloat(data[i].NET_TOTAL).toFixed(2)) + '</td></tr>');
        $('#sales-detail-data').append(row);
        grandTotal += data[i].NET_TOTAL;
    }

    $("#location-name-sales-detail").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-sales-detail").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    $('#sales-detail-grand-total').text(numberWithCommas(parseFloat(grandTotal).toFixed(2)));

    $.print("#sales-detail-report");
}

$('#item-sales-detail').click(function (e) {

    $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/SelectItemSalesDetail", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: ShowItemSalesDetail
                }
            );
});

function ShowItemSalesDetail(data) {
    data = JSON.stringify(data);
    data = jQuery.parseJSON(data.replace(/&quot;/g, '"'));
    data = eval(data.d);
    $("#lblAddress3").text($("#hfAddress").val() + ' Ph: ' + $("#hfPhoneNo").val());
    $('#item-sales-detail-data').empty(); // clear all skus  from invoice
    var grandTotal = 0;
    for (var i = 0, len = data.length; i < len; i++) {
        var row = $(' <tr><td style="width:65%;">' + data[i].SKU_NAME + '</td><td  style="width:15%; text-align:center;" >' + data[i].QUANTITY + '</td><td style="width:20%;" class="text-right">' + numberWithCommas(parseFloat(data[i].NET_TOTAL).toFixed(2)) + '</td></tr>');
        $('#item-sales-detail-data').append(row);
        grandTotal += data[i].NET_TOTAL;
    }

    $("#location-name-item-sales-detail").text(data[0].DISTRIBUTOR_NAME);
    $("#current-work-date-item-sales-detail").text($("#hfCurrentWorkDate").val() + ' ' + moment().format('hh:mm A'));

    $('#item-sales-detail-grand-total').text(numberWithCommas(parseFloat(grandTotal).toFixed(2)));

    $.print("#item-sales-detail-report");
}

//------------------------------------------------------------------------------\\

//----------------Can enter only numbers and decimal ---------------------------\\

function onlyNumbers(txt, event) {
    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}
function onlyDotsAndNumbers(txt, event) {



    var charCode = (event.which) ? event.which : event.keyCode;

    if (charCode == 9 || charCode == 8) {
        return true;
    }
    if (charCode == 46) {
        if (txt.value.indexOf(".") < 0)
            return true;
        return false;
    }
    if (charCode == 31 || charCode < 48 || charCode > 57)
        return false;

    return true;
}

//------------------------------------------------------------------------------\\

//#region Time Calculation

function BillTime() {

    var refresh = 1000; // Refresh rate in milli seconds

    mytime = setTimeout('display_ct()', refresh)

    GetPendingBills();

   // var $rows = $('#tbl-customers tr');

    $('#txtCustomerSearch').keyup(function () {

       // var val = $.trim($(this).val()).replace(/ +/g, ' ').toLowerCase();

      //  $rows.show().filter(function () {

          ///  var text = $(this).text().replace(/\s+/g, ' ').toLowerCase();

           // return !~text.indexOf(val);
        //   }).hide();
        if ($('#txtCustomerSearch').val().length > 3) {
            LoadAllCustomers();
        }
        else if ($('#txtCustomerSearch').val().length < 3) {
            $("#tbl-customers").empty();
        }
    });

}

//--Used in BillTime();
function display_ct() {
    var strcount
    var d = new Date()

    var h = addZero(d.getHours());
    var m = addZero(d.getMinutes());

    document.getElementById('ct').innerHTML = h + ":" + m;
    tt = BillTime();
}

//--Used In display_ct();
function addZero(i) {//Used For Adding 0 Before Hour When Hour is Less than 10
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

//--Used In GetPendingBills();
function diff(start, end) {//Get The updated Time For Hold Orders 
    start = start.split(":");
    end = end.split(":");
    var startDate = new Date(0, 0, 0, start[0], start[1], 0);
    var endDate = new Date(0, 0, 0, end[0], end[1], 0);
    var diff = endDate.getTime() - startDate.getTime();
    var hours = Math.floor(diff / 1000 / 60 / 60);
    diff -= hours * 1000 * 60 * 60;
    var minutes = Math.floor(diff / 1000 / 60);

    return (hours < 10 ? "0" : "") + hours + ":" + (minutes < 10 ? "0" : "") + minutes;
}

//#endregion Time Calculation


//#region Customer

function lnkCustomerType(obj) {
    var id = obj.id;

    waitLoading('Loading');

    $("#lnkDelivery").removeClass("active");
    $("#lnkTakeaway").removeClass("active");
    $("#lnkDineIn").removeClass("active");

    if (id == "lnkDelivery") {
        $("#lnkDelivery").addClass("active");

        document.getElementById('divCustomer').style.visibility = 'visible';

        document.getElementById("hfCustomerType").value = "Delivery";
        document.getElementById('dvTableList').style.display = "none";
        document.getElementById('dvPendingBills').className = 'col-md-12 vac-det';

        $("#lblSaleForce").text("Delivery Man");

        $("#hfTableNo").val("");
        $("#TableNo1").text("N/A");
        $("#OrderNo1").text("N/A");
        $("#txtCoverTable").val('');

        loadSaleForce();

        $('#txtCustomerSearch').val('');
        $("#tbl-customers").empty();
    }
    else if (id == "lnkTakeaway") {

        $("#lnkTakeaway").addClass("active");

        document.getElementById('divCustomer').style.visibility = 'hidden';

        document.getElementById("hfCustomerType").value = "Takeaway";
        document.getElementById('dvTableList').style.display = "none";
        document.getElementById('dvPendingBills').className = 'col-md-12 vac-det';

        $("#lblSaleForce").text("Order Taker");
        loadSaleForce();
    }
    else {
        $("#lnkDineIn").addClass("active");

        document.getElementById('divCustomer').style.visibility = 'hidden';

        document.getElementById("hfCustomerType").value = "Dine In";

        document.getElementById('dvTableList').style.display = "block";
        document.getElementById('dvPendingBills').className = 'col-md-4 vac-det';

        $("#lblSaleForce").text("Order Taker");
        loadSaleForce();

        NewOrder();
    }
    GetPendingBills();

}

function SaveCustomer() {

    if (ValidateCustomer()) {

        $.ajax
            (
                {
                    type: "POST", //HTTP method
                    url: "frmOrderPOS.aspx/InsertCustomer", //page/method name
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ cardID: $('#txtCustomerCardNo').val(), CNIC: $('#txtCustomerCNIC').val(), contactNumer: $('#txtPrimaryContact').val(), contactNumer2: $('#txtOtherContact').val(), customerName: $('#txtCustomerName').val(), address: $('#txtCustomerAddress').val(), DOB: $('#txtCustomerDOB').val() }),
                    success: ClearCustomer,
                    error: OnError
                }
            );
    }
}
function ValidateCustomer() {
    if ($('#txtCustomerName').val() == "") {
        Error("Must enter Customer Name");

        return false;
    }
    if ($('#txtPrimaryContact').val() == "") {
        Error("Must enter Primary Contact");

        return false;
    }
    if ($('#txtCustomerAddress').val() == "") {
        Error("Must enter Address");

        return false;
    }
    return true;
}
function ClearCustomer() {
    $('#txtCustomerCardNo').val('');
    $('#txtPrimaryContact').val('');
    $('#txtOtherContact').val('');
    $('#txtCustomerName').val('');
    $('#txtCustomerAddress').val('');
    $('#txtCustomerCNIC').val('');
    $('#txtCustomerDOB').val('');
    $('#txtCustomerSearch').val('');

    document.getElementById('divCustomer').style.visibility = 'hidden';

    LoadCustomers();

}
function CancelCustomer() {

    $('#txtCustomerCardNo').val('');
    $('#txtPrimaryContact').val('');
    $('#txtOtherContact').val('');
    $('#txtCustomerName').val('');
    $('#txtCustomerCNIC').val('');
    $('#txtCustomerDOB').val('');
    $('#txtCustomerAddress').val('');

    // $('#divCustomer').css({ opacity: 0, visibility: "visible" }).animate({ opacity: 1 }, 'slow');
    document.getElementById('divCustomer').style.visibility = 'hidden';
    // document.getElementById('divCustomer').className = "hideCustomer"; // Fade out

}

//On KEY UP txtCustomerSearch
function LoadAllCustomers() {
     $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadAllCustomers", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ customerName: $('#txtCustomerSearch').val(), type: "Search" }),
                success: LoadAllCustomer
            }
  );
}
function LoadAllCustomer(customers) {

    customers = JSON.stringify(customers);
    var result = jQuery.parseJSON(customers.replace(/&quot;/g, '"'));

    customers = eval(result.d);
    $("#tbl-customers").empty();

    for (var i = 0, len = customers.length; i < len; i++) {
        var row = $('<tr ><td style="display:none;">' + customers[i].CUSTOMER_ID + '</td><td class="leftval">' + customers[i].CUSTOMER_NAME + '</td><td class="leftval">' + customers[i].CUSTOMER_CODE + '</td><td class="leftval">' + customers[i].CONTACT_NUMBER + '</td><td class="leftval"  style="display:none;">' + customers[i].EMAIL_ADDRESS + '</td><td class="leftval"  style="display:none;">' + customers[i].CNIC + '</td><td class="leftval"  style="display:none;">' + customers[i].REGDATE + '</td><td class="leftval">' + customers[i].ADDRESS + '</td><td style="display:none;" align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td>' + '<td align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-share-square-o"></span></a></td></tr>');
        $('#tbl-customers').append(row);
    }
}

//On Save Customer
function LoadCustomers() {
    $.ajax
        (
            {
                type: "POST", //HTTP method
                url: "frmOrderPOS.aspx/LoadAllCustomers", //page/method name
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ customerName: "", type:"Max" }),
                success: LoadLastCustomer
            }
        );
}
function LoadLastCustomer(customers) {

    customers = JSON.stringify(customers);
    var result = jQuery.parseJSON(customers.replace(/&quot;/g, '"'));

    customers = eval(result.d);

    $("#tbl-customers").empty();

    for (var i = 0, len = customers.length; i < len; i++) {

        var row = $('<tr ><td style="display:none;">' + customers[i].CUSTOMER_ID + '</td><td class="leftval">' + customers[i].CUSTOMER_NAME + '</td><td class="leftval">' + customers[i].CUSTOMER_CODE + '</td><td class="leftval">' + customers[i].CONTACT_NUMBER + '</td><td class="leftval"  style="display:none;">' + customers[i].EMAIL_ADDRESS + '</td><td class="leftval"  style="display:none;">' + customers[i].CNIC + '</td><td class="leftval" style="display:none;">' + customers[i].REGDATE + '</td><td class="leftval">' + customers[i].ADDRESS + '</td><td style="display:none;" align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-pencil-square-o"></span></a></td>' + '<td align="center" onclick="ShowCustomer(this);"><a href="#"><span class="fa fa-share-square-o"></span></a></td></tr>');
        $('#tbl-customers').append(row);
    }

    var table = document.getElementById('tbl-customers');
    var lastRow = table.rows[table.rows.length - 1];

    ShowCustomer2(lastRow);

    HoldOrder();

    LoadLastBill();
}


//After Insertion Select Last Customer
function ShowCustomer2(rowIndex) {

    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    document.getElementById('hfTableNo').value = $(rowIndex).find('td:eq(1)').text();
    document.getElementById('divCustomer').style.visibility = 'hidden';
}

//Get Customer Detail on Selection Button on row click
function ShowCustomer(obj) {
    var rowIndex = $(obj).parent();

    //$('#txtCustomerName').val($(rowIndex).find('td:eq(1)').text());
    //$('#txtCustomerCardNo').val($(rowIndex).find('td:eq(2)').text());
    //$('#txtPrimaryContact').val($(rowIndex).find('td:eq(3)').text());
    //$('#txtOtherContact').val($(rowIndex).find('td:eq(4)').text());
    //$('#txtCustomerCNIC').val($(rowIndex).find('td:eq(5)').text());


    document.getElementById('hfCustomerId').value = $(rowIndex).find('td:eq(0)').text();
    document.getElementById('hfTableNo').value = $(rowIndex).find('td:eq(1)').text();
    document.getElementById('divCustomer').style.visibility = 'hidden';

    $('#txtCustomerSearch').val('');

    HoldOrder();

    LoadLastBill();
}

function LoadLastBill() {
    setTimeout(function () {

        var table = document.getElementById('tbl-pending-bills');
        var lastRow = table.rows[table.rows.length - 1];


        if ($(lastRow).find("td:eq(10)").text() == "DLY") {

            var amountDue = $(lastRow).find("td:eq(2)").text();

            if (amountDue == 0) {

                ShowBill(lastRow);
            }
        }
        else {
            ShowBill(lastRow);
        }
    }, 1000);
}

//#endregion



