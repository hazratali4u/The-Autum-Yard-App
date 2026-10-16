<div class="container" id="payment2" style="max-width: 700px; display: none; z-index: 5000;
    top: 30%; left: 25%; position: absolute; background-color: #c6c6c6; padding: 10px;
    border: 1px solid #000;">
    <div class="row">
        <div class="col-md-12" style="margin-top:10px;">
            <div class="col-md-6">
                
                <div class="col-md-12">
                    <label id="subTotal2" style="font: bold 32px sans-serif; display: none; color: #fff;
                        background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>
                    
                    <label id="SalesTax2" style="font: bold 18px sans-serif; display: none; color: #fff;
                        background-color: #000; text-align: right; display: none;">
                        0.00
                    </label>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Gross Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="GrandTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                   
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Discount Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblDiscountTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                     <div class="col-md-8" style="margin-top: 7px;">
                     
                             <label runat="server" id="lblGSTOrService2" style="font: bold 17px sans-serif;">
                             GST Amount
                        </label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblGSTTotal2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                    <div class="col-md-8" style="margin-top: 7px;">
                        <label style="font: bold 17px sans-serif;">
                            Payment Due</label>
                    </div>
                    <div class="col-md-4">
                        <label runat="server" id="lblPaymentDue2" style="font: bold 32px sans-serif; text-align: right;">
                            0.00
                        </label>
                    </div>
                </div>
            </div>
            <div class="col-md-6" style="border-left: 1px solid #dadada;">
                <div class="col-md-12">
                    <div class="text">
                        Discount Type</div>
                    <div class="col-md-5">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px;
                            color: #FFF; background-color: #919399;" onclick="DiscType(this);" id="percentage2" 
                            value="0">
                            %age</button></div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-5">
                        <button type="button" class="btn btn-toolbar" style="min-width: 120px; font-size: 15px;
                            color: #FFF; background-color: #919399;" onclick="DiscType(this)" id="value2" 
                            value="1">
                            Value</button></div>
                </div>
                <div class="col-md-12" style="margin-top: 5px;">
                    <div class="col-md-4">
                        <label>
                            Discount</label>
                        <input type="text" id="txtDiscount2" runat="server" class="form-control" style="font-size: 20px;
                            font-weight: bold;"  onkeyup="CalculateBalance2();" value="0" onkeypress="return onlyDotsAndNumbers(this,event);" disabled="disabled"/>
                    </div>
                    <div class="col-md-8">
                        <label>
                            Authorized By
                        </label>
                        <input type="text" id="txtApprovedBy2" runat="server" class="form-control"  disabled="disabled"/>
                    </div>
                </div>
                 <div class="col-md-12" style="margin-top: 5px;">
                     </div>
                 <div class="col-md-12" style="margin-top: 5px; display:none;" runat="server"  id="dvServiceCharges2">
                    <div class="col-md-4">
                        <label>
                            Service Charges(%)</label>
                    </div>
                    <div class="col-md-8">
                       
                        <input type="text" id="txtService2" runat="server" class="form-control" style="font-size: 20px;
                            font-weight: bold;"  onkeyup="CalculateBalance2();" onkeypress="return onlyDotsAndNumbers(this,event);" />
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="col-md-6">
                </div>
                <div class="col-md-6">
                    <div class="col-md-5 btn-mar">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -143px;
                            font-size: 15px; color: #FFF; background-color: #3c8d75;" id="btnSave2" onclick="UpdateOrder();">
                            Save</button>
                    </div>
                    <div class="col-md-5 btn-mar" style="margin-left: 10px;">
                        <button class="btn btn-toolbar" type="button" style="min-width: 100px; margin-left: -133px;
                            font-size: 15px; color: #FFF; background-color: #f94d72;" id="btnCancel2" onclick="ClearOnCancel2();">
                            Cancel</button>
                    </div>
                    <div class="col-md-5" style="margin-top: 15px;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</div>

