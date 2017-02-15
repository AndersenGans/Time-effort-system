function addTimeReport() {
    $.ajax({
        url: window.CREATE_REPORT_URL,
        type: "GET",
        success: function (returnedData) {
            var currentRow = $("#timeReportRow");

            if (currentRow.length > 0) {
                currentRow.replaceWith(returnedData);
            } else {
                $(".table tr:last").after(returnedData);
            }
        }
    });
}


function editTimeReport(reportId, elem) {
    $.ajax({
        url: window.EDIT_REPORT_URL,
        type: "GET",
        data: { "id": reportId },
        success: function(data) {
            //$("input[name='reportId'][value='" + reportId + "']").parent('tr').replaceWith(data);
            $(elem).parent('td').parent('tr').replaceWith(data);
        }
    });
}


function saveTimeReport() {
    debugger;
    var formData = $("#timeReportForm").serializeArray();
    $.ajax({
        url: window.SAVE_REPORT_URL,
        type: "POST",
        data: formData,
        success: function () {
            location.reload();
        },
        error: function (xhr) {
            alert(xhr.statusText);
        }
    });
}
