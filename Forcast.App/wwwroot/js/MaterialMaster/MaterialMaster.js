$(function () {
    var currentPage = 1;
    var pageSize = 10;

    fetchMaterials(currentPage, pageSize);

    // Revised Event Handlers with modern jQuery 'on' syntax
    $(document).on('click', '.edit-button', function () {
        var materialId = $(this).data('material-id');
        console.log(materialId);
        editMaterial(materialId);
    });

    $('#saveButton').on('click', function () {
        saveMaterial();
    });

    $('#uploadForm').on('submit', function (e) {
        e.preventDefault();
        uploadFile();
    });

    $('#editModal').on('shown.bs.modal', function () {
        $(this).find('.close').on('click', function () {
            $('#editModal').modal('hide');
        });
    });

    $('#materialTable').on('click', '.delete-button', function () {
        var materialId = $(this).data('material-id');
        deleteMaterial(materialId);
    });

    $('#uploadButton').on('click', uploadFile);

    $('#addMaterialButton').on('click', function () {
        addMaterial();
    });

    function fetchMaterials(page, size) {
        var url = `/GetPagedMaterials?page=${page}&pageSize=${size}`;
        $.get(url, function (data) {
            console.log(data);
            renderMaterialList(data.items);
            setupPagination(data.totalCount, data.currentPage, data.pageSize);
        }).fail(function (xhr) {
            console.error('Error fetching material data:', xhr.responseText);
        });
    }

    function renderMaterialList(materialList) {
        console.log(materialList);
        var materialTable = $('#materialTable tbody');
        materialTable.empty();

        if (materialList && Array.isArray(materialList)) {
            materialList.forEach(function (material) {
                materialTable.append(`
                        <tr>
                            <td class="material-id">${material.id}</td>
                            <td>${material.material}</td>
                            <td>${material.description}</td>
                            <td>${material.dpName}</td>
                            <td>
                                <button class="btn btn-warning edit-button" data-material-id="${material.id}" title="Edit">
                                    <i class="fas fa-edit"></i>
                                </button>
                                <button class="btn btn-danger delete-button" data-material-id="${material.id}" title="Delete">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                            </td>
                        </tr>
                        `);
            });
        } else {
            console.error("Received material list is undefined or not an array:", materialList);
            materialTable.append('<tr><td colspan="5">No materials found.</td></tr>');
        }
    }

    function setupPagination(totalItems, currentPage, pageSize) {
        var totalPages = Math.ceil(totalItems / pageSize);
        var pagination = $('.pagination');
        pagination.empty(); // Clear existing pagination controls

        // Append 'Previous' button
        pagination.append(`<li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
                        <a class="page-link" href="#" onclick="changePage('prev', ${totalPages})">Previous</a>
            </li>`);

        // Append 'Next' button
        pagination.append(`<li class="page-item ${currentPage >= totalPages ? 'disabled' : ''}">
                        <a class="page-link" href="#" onclick="changePage('next', ${totalPages})">Next</a>
            </li>`);
    }

    window.changePage = function (page, totalPages) {
        if (page === 'prev' && currentPage > 1) {
            currentPage -= 1;
        } else if (page === 'next' && currentPage < totalPages) {
            currentPage += 1;
        }

        fetchMaterials(currentPage, pageSize); // Fetch materials using the new currentPage
    }

    function editMaterial(materialId) {
        $.ajax({
            url: `/MaterialMaster/GetMaterialById`, // Cập nhật URL với route parameter
            type: 'GET',
            data: { id: materialId },
            success: function (material) { // Giả định rằng phản hồi thành công trả về trực tiếp đối tượng material
                if (material) { // Kiểm tra trạng thái thành công trong đối tượng JSON trả về
                    console.log(material);
                    $('#editId').val(material.data.id);
                    $('#editMaterial').val(material.data.material);
                    $('#editDescription').val(material.data.description);
                    $('#editDpName').val(material.data.dpName);
                    $('#editModal').modal('show');
                } else {
                    alert('Material not found.');
                }
            },
            error: function (xhr, status, error) {
                alert('Error fetching material details: ' + error);
            }
        });
    }

    //function saveMaterial() {
    //    var formData = $('#editForm').serializeArray();
    //    var dataObject = {
    //        Id: $('#editId').val(),  // Make sure values are correctly parsed as integers where necessary
    //        Material: parseInt($('#editMaterial').val(), 10),
    //        Description: $('#editDescription').val(),
    //        DpName: $('#editDpName').val()
    //    };

    //    $.ajax({
    //        url: `https://localhost:7083/api/MaterialMaster/UpdateMaterialMaster/${dataObject.Id}`,
    //        type: 'PUT',
    //        contentType: 'application/json',
    //        data: JSON.stringify(dataObject),  // Ensure that you are sending the dataObject directly
    //        success: function (material) {
    //            console.log("Received material from server:", material);
    //            alert('Material updated successfully.');
    //            $('#editModal').modal('hide');
    //            updateMaterialRow(material);
    //        },
    //        error: function (xhr, status, error) {
    //            console.error('Error updating material:', xhr.responseText);
    //            alert('Error updating material: ' + xhr.responseText);
    //        }
    //    });

    //}
    function saveMaterial() {
        var dataObject = {
            Id: parseInt($('#editId').val(), 10),
            Material: parseInt($('#editMaterial').val(), 10),
            Description: $('#editDescription').val(),
            DpName: $('#editDpName').val()
        };

        $.ajax({
            url: `MaterialMaster/UpdateMaterialMaster/${dataObject.Id}`, // Include the ID in the URL
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(dataObject),
            success: function (material) {
                console.log("Received material from server:", material);
                alert('Material updated successfully.');
                $('#editModal').modal('hide');
                updateMaterialRow(dataObject);  // Ensure this function is implemented to reflect changes in UI
            },
            error: function (xhr, status, error) {
                console.error('Error updating material:', xhr.responseText);
                alert('Error updating material: ' + xhr.responseText);
            }
        });
    }


    function updateMaterialRow(material) {
        console.log("Attempting to update row with ID:", material);
        var row = $('#materialTable tbody').find('tr').filter(function () {

            if (material && material.Id !== undefined) {
                return $(this).find('.material-id').text().trim() == material.Id.toString().trim();
            } else {
                console.error('Material or material ID is undefined');
                return false;
            }

        });

        console.log("Found rows:", row.length);
        if (row.length > 0) {
            row.find('td:eq(1)').text(material.Material);
            row.find('td:eq(2)').text(material.Description);
            row.find('td:eq(3)').text(material.DpName);
        } else {
            console.log("No matching row found for ID:", material.Id);
        }
    }


    function deleteMaterial(id) {
        if (confirm('Are you sure you want to delete this material?')) {
            $.ajax({
                url: `/MaterialMaster/DeleteMaterial/${id}`,  // Thay đổi URL để phù hợp với route controller
                type: 'DELETE',
                success: function (response) {
                    alert('Material deleted successfully.');
                    fetchMaterials(currentPage, pageSize);
                },
                error: function (xhr, status, error) {
                    alert('Error deleting material: ' + error);
                }
            });
        }
    }


    function uploadFile() {
        var formData = new FormData();
        var fileInput = $('#file')[0];
        var file = fileInput.files[0];

        if (!file) {
            alert('Please select a file to upload');
            return;
        }

        formData.append('file', file);

        $.ajax({
            url: 'https://localhost:7083/api/MaterialMaster/UploadFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                alert('File uploaded successfully: ' + data);
            },
            error: function (xhr, status, error) {
                console.error('Error uploading file:', error);
                alert('Error uploading file: ' + error);
            }
        });
    }

    function addMaterial() {
        var formData = {
            Material: parseInt($('#material').val(), 10),
            Description: $('#description').val(),
            DpName: $('#dpName').val()
        };
        $.ajax({
            url: '/MaterialMaster/AddMaterial',  // Đổi URL phù hợp với route đã định nghĩa trong controller
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                alert('Material added successfully');
                console.log(response);
            },
            error: function (xhr, status, error) {
                alert('Error adding material: ' + error);
                console.error(xhr.responseText);
            }
        });
    }

});
