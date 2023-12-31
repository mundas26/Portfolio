﻿var dataTable; 
$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url:'/admin/project/getall'},
        "columns": [
            /*{ data: 'imageUrl', "render": function (data) { return '<img src="' + data + '" alt="' + data + '"height="50px" width="50px"/>'; }},*/
            { data: 'title', "width": "35%" },
            { data: 'dateCreated', "width": "15%"},
            { data: 'category.name', "width": "10%" },
            { data: 'id',"render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href= "/admin/project/upsert?id=${data}" class="btn btn-primary mx-2 rounded-4"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <a OnClick=Delete('/admin/project/delete/${data}') class="btn btn-danger mx-2 rounded-4"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                 "width": "15%"   
            },
        ],
        "scrollX": true,
        "responsive": true
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}