
$(document).ready(function () {

    //Add button click event
    $('#add').click(function () {

        var s = $('.deptno').val();

        //validation and add records
        var isAllValid = true;

        if (!($('#empno').val().trim() != '' && !isNaN($('#empno').val().trim()))) {
            isAllValid = false;
            $('#empno').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#empno').siblings('span.error').css('visibility', 'hidden');
        }



        if (!($('#ename').val().trim() != '')) {
            isAllValid = false;
            $('#ename').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#ename').siblings('span.error').css('visibility', 'hidden');
        }

        if (!($('#job').val().trim() != '')) {
            isAllValid = false;
            $('#job').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#job').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#hiredate').val().trim() != '')) {
            isAllValid = false;
            $('#hiredate').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#hiredate').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#sal').val().trim() != '' && !isNaN($('#sal').val().trim()))) {
            isAllValid = false;
            $('#sal').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#sal').siblings('span.error').css('visibility', 'hidden');
        }


        if (!($('#deptno').val().trim() != '' && !isNaN($('#deptno').val().trim()))) {
            isAllValid = false;
            $('#deptno').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#deptno').siblings('span.error').css('visibility', 'hidden');
        }



        if (isAllValid) {
            var $newRow = $('#mainrow').clone().removeAttr('id');
            //$('.pc', $newRow).val($('#productCategory').val());
            //$('.product', $newRow).val($('#product').val());

            //Replace add button with remove button
            //$('#add', $newRow).addClass('remove').val('Remove').removeClass('btn-success').addClass('btn-danger');
            $('#deptno', $newRow).val(s);
            s = null;

            //remove id attribute from new clone row
            $('#empno,#ename,#job,#mgr,#hiredate,#sal,#comm,#deptno,#add', $newRow).removeAttr('id');
            $('span.error', $newRow).remove();
            //append clone row
            $('#orderdetailsItems').append($newRow);

            //clear select data
            //$('#productCategory,#product').val('0');
            //$('#quantity,#rate').val('');


            var empno = parseInt($('#empno').val()) + 1;
            $('#empno').val(empno);
            $('#ename,#job,#mgr,#hiredate,#sal,#comm,#deptno').val('');
            $('#orderItemError').empty();

            $('#orderdetailsItems tr td a.add').hide();

            $('#orderdetailsItems tr td a.delete').removeClass('collapse').addClass('collapse.in');
        }

    })

    //remove button click event
    $('#orderdetailsItems').on('click', '.delete', function () {
        $(this).parents('tr').remove();
    });

    $('#submit').click(function () {
        var isAllValid = true;

        //validate order items
        $('#orderItemError').text('');
        var list = [];
        var errorItemCount = 0;
        $('#orderdetailsItems tbody tr').each(function (index, ele) {
            if (
                //$('select.product', this).val() == "0" ||
                $('.empno', this).val() == "" ||
                isNaN($('.empno', this).val()) ||
                $('.ename', this).val() == "" ||
                $('.job', this).val() == "" ||
                $('.hiredate', this).val() == "" ||
                $('.sal', this).val() == "" ||
                isNaN($('.sal', this).val()) ||
                $('.deptno', this).val() == "" ||
                isNaN($('.deptno', this).val())

                //$('.rate', this).val() == "" ||
                //isNaN($('.rate', this).val())
                ) {
                errorItemCount++;
                $(this).addClass('error');
            } else {
                var orderItem = {
                    //ProductID: $('select.product', this).val(),
                    //Quantity: parseInt($('.quantity', this).val()),
                    //Rate: parseFloat($('.rate', this).val())
                    EMPNO: $('.empno', this).val(),
                    ENAME: $('.ename', this).val(),
                    JOB: $('.job', this).val(),
                    MGR: $('.mgr', this).val(),
                    HIREDATE: $('.hiredate', this).val(),
                    SAL: $('.sal', this).val(),
                    COMM: $('.comm', this).val(),
                    DEPTNO: $('.deptno', this).val(),
                }
                JSON.stringify(orderItem)
                list.push(orderItem);
            }
        })
        debugger

        if (errorItemCount > 0) {
            $('#orderItemError').text(errorItemCount + " invalid entry in record.");
            isAllValid = false;
        }

        if (list.length == 0) {
            $('#orderItemError').text('At least 1 record required.');
            isAllValid = false;
        }

        if ($('#dept').val().trim() == '') {
            $('#dept').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#dept').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#dname').val().trim() == '') {
            $('#dname').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#dname').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#loc').val().trim() == '') {
            $('#loc').siblings('span.error').css('visibility', 'visible');
            isAllValid = false;
        }
        else {
            $('#loc').siblings('span.error').css('visibility', 'hidden');
        }
        if (isAllValid) {

            var datas = {
                Deptid: $('#dept').val().trim(),
                Dname: $('#dname').val().trim(),
                Location: $('#loc').val().trim(),
                //OrderDetails: list
                EMP: list
            }
            debugger
            //$(this).val('Please wait...');

            $.ajax({
                type: 'POST',
                url: '/Home/Save',
                //data: datas,
                //contentType: 'application/json',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ Deptid: datas.Deptid, Dname: datas.Dname, EMP: datas.EMP, Location: datas.Location }),
                success: function (data) {
                    if (data.status) {
                        window.location.href = (data.newurl);
                        //alert('Successfully saved');
                        //  here we will clear the form/
                        list = [];
                        $('#dept,#dname,#loc').val('');
                        $('#orderdetailsItems').empty();
                    }
                    else {
                        alert('Error');
                    }

                    $('#submit').text('Save');
                },
                error: function (error) {
                    console.log(error);
                    $('#submit').text('Save');
                }
            });
        }
    })
})