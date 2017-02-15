//Выделение всех чекбоксов, при нажатии на чекбокс("Выделить всё!")
function Check(){
    if ($("input.checkALL").prop("checked")) {
        $("input.timeReports").prop("checked", true);
    } else {
        $("input.timeReports").prop("checked", false);
    }
}

function NotifySelected() {
    var reports = [];
    $("input.timeReports[type='checkbox']:checked").each(function () {
        reports.push($(this).parent().prev().val());
    });
    $.ajax({
        url: window.NOTIFY_REPORT_URL,
        type: "POST",
        data: { reports: reports },
        success: function () {
            $("input[type='checkbox']:checked").each(function () {
                $("input[type='checkbox']").prop("checked", false);
            });
            window.location.reload();
        }
    });
}

//////---Не моё!--
//$(".checkALL").change(function () {
//    if ($(".checkALL").prop("checked")) {
//        $(".timeReports").not(":disabled").prop("checked", true);
//        $(".timeReports").not(":disabled").prop("id", 2);

//    } else {
//        $(".timeReports").prop("checked", false);
//        $(".timeReports").prop("id", 1);
//    }
//});

//$(".timeReports").change(function () {
//    if ($(".timeReports").not(":disabled").prop("checked")) {
//        $(".timeReports").prop("id", 2);
//    } else {
//        $(".timeReports").prop("id", 1);
//    }
//});

//(function accept(elements) {
//    if ($(".timeReports").not(":disabled").prop("id") == 2) {
//        $.ajax({
//            url: window.DECLINE_REPORT_URL,
//            success: function () {
//                if ($(".timeReports").prop("name") == $(elements).prop("Id")) {
//                    $(elements).prop("Status", "Accept");
//                }
//            }
//        });
//    }
//});

//Страница Edit project
var countTeamMembers = 0;
var countRoles = 0;
function AddTeamMember() {
    var valueTeamMember = $("[id = 'dropDownListTeamMember'] option:selected").text();
    var valueRoleInProject = $("[id = 'dropDownListRole'] option:selected").text();
    var idTeamMember = $("[id = 'dropDownListTeamMember']").val();
    var idRoleInProject = $("[id = 'dropDownListRole']").val();
    $("table#AddTeamMember").append("<tr class = 'GenerationHtmlCode'><input type = 'hidden' value = " + countTeamMembers + " disabled/><input type = 'hidden' value = " + idTeamMember + " name = 'idTeamMember' form = 'form'/><input type = 'hidden' value = " + countRoles + " disabled/><input type = 'hidden' value = " + idRoleInProject + "  name = 'idRoleInProject'  form = 'form'/><td>" + valueTeamMember + "</td><td>" + valueRoleInProject + "</td><td><input type='button' value='Delete' class='btn btn-default' onclick='DeleteHtml(this)'/></td></tr>");
    countRoles++;
    countTeamMembers++;
}
var countTasks = 0;
function AddTasks() {
    var valueTasks = $("[id = 'dropDownListTasks'] option:selected").text();
    var idTasks = $("[id = 'dropDownListTasks']").val();
    $("table#AddTasks").append("<tr class = 'GenerationHtmlCode'><input type = 'hidden' value = " + countTasks + " disabled/><input type = 'hidden' value = " + idTasks + " name = 'idTasks' form = 'form'/><td>" + valueTasks + "</td><td><input type='button' value='Delete' class='btn btn-default' onclick='DeleteHtml(this)'/></td></tr>");
    countTasks++;
}

function DeleteHtml(e) {
    e.parentNode.parentNode.remove();
}

//Фильтры на ProjectManagement

//function FilterSelect(elem, selectName) {
//    $.ajax({
//        url: window.PROJECT_SELECT_URL,
//        type: "GET",
//        success: function () {
//            if (selectName == "ProjectName") {
//                $('#ProjectManagement_Select').replaceWith(
//                    '<select class="select" style="height: 25px" id="ProjectManagement_Project" onchange="FilterProjectName(this, this.value)">' +
//                        '@foreach (ProjectManagementModel project in Model)' +
//                        '{' +
//                            '<option value="@project.Id">@project.ProjectName</option>' +
//                        '}' +
//                    '</select>');
//            }
//            else {
//                $('#ProjectManagement_Select').replaceWith(
//                    '<select class="select" style="height: 25px" id="ProjectManagement_Manager" onchange="FilterProjectManager(this, this.value)">' +
//                        '@foreach (ProjectManagementModel project in Model)' +
//                        '{' +
//                            '<option value="@project.Id">@project.ProjectManager</option>' +
//                        '}' +
//                    '</select>');
//            }

//        }
//    });
//}


function FilterProjectName(elem, id) {
    $.ajax({
        url: window.PROJECT_NAME_URL,
        type: "GET",
        data: { id: id },
        success: function (data) {
            $('table.table').replaceWith(data);
        }
    });
}

function FilterProjectManager(elem, name) {
    debugger;
    $.ajax({
        url: window.PROJECT_MANAGER_URL,
        type: "GET",
        data: { name: name },
        success: function (data) {
            $('table.table').replaceWith(data);
        }
    });
}


function FilterWeekOrMonth(elem, value) {
    $.ajax({
        url: window.FilterWeekOrMonth_URL,
        type: "GET",
        data: { value: value },
        success: function (data) {
            $('table.table').replaceWith(data);
        }
    });
}


function FilterDate(elem, value) {
    $.ajax({
        url: window.FilterDate_URL,
        type: "GET",
        data: { value: value },
        success: function (data) {
            $('table.table').replaceWith(data);
        }
    });
}

function FilterDateEnd(elem, value) {
    $.ajax({
        url: window.FilterDateEnd_URL,
        type: "GET",
        data: { value: value },
        success: function (data) {
            $('table.table').replaceWith(data);
        }
    });
}

