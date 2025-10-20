import React, { useState, useEffect, useCallback } from 'react';
import DataTable from 'react-data-table-component';
import { Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import apiService from '../services/apiService';
import VehicleServiceRecordFormModal from '../components/VehicleServiceRecordFormModal';
import DeleteConfirmationModal from '../components/DeleteConfirmationModal';

const HomePage = () => {
  const [records, setRecords] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalRows, setTotalRows] = useState(0);
  const [perPage, setPerPage] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchText, setSearchText] = useState('');
  const [sortColumn, setSortColumn] = useState('ServiceDate');
  const [sortDirection, setSortDirection] = useState('desc');

  const [showFormModal, setShowFormModal] = useState(false);
  const [editRecordId, setEditRecordId] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [deleteId, setDeleteId] = useState(null);
  const [modalKey, setModalKey] = useState(0);

  const fetchRecords = useCallback(async (page, size, search, sortCol, sortDir) => {
    setLoading(true);
    const request = {
      Draw: page,
      SortingPaging: {
        SortItem: {
          ColumnName: sortCol,
          ColumnOrder: sortDir === 'asc' ? 'Ascending' : 'Descending',
        },
        PageNumber: page,
        NumberRecords: size,
      },
      SearchText: search || '',
    };

    const response = await apiService.getVehicleServiceRecords(request);
    if (response?.Result?.ResultCode === 0) {
      setRecords(response.ReturnObject || []);
      setTotalRows(response.TotalCount || 0);
    } else {
      toast.error(response?.Result?.ResultMessage || 'Kayıtlar yüklenirken bir hata oluştu', { position: "bottom-right" });
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    fetchRecords(currentPage, perPage, searchText, sortColumn, sortDirection);
  }, [fetchRecords, currentPage, perPage, searchText, sortColumn, sortDirection]);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handlePerRowsChange = (newPerPage, page) => {
    setPerPage(newPerPage);
    setCurrentPage(page);
  };

  const handleSort = (column, direction) => {
    setSortColumn(column.selector);
    setSortDirection(direction);
  };

  const handleSearchChange = (e) => {
    setSearchText(e.target.value);
    setCurrentPage(1);
  };

  const openCreateModal = () => {
    setEditRecordId(null);
    setModalKey(prevKey => prevKey + 1);
    setShowFormModal(true);
  };

  const openEditModal = (id) => {
    setEditRecordId(id);
    setModalKey(prevKey => prevKey + 1);
    setShowFormModal(true);
  };

  const closeFormModal = () => {
    setShowFormModal(false);
    setEditRecordId(null);
  };

  const openDeleteConfirmation = (id) => {
    setDeleteId(id);
    setShowDeleteModal(true);
  };

  const closeDeleteConfirmation = () => {
    setShowDeleteModal(false);
    setDeleteId(null);
  };

  const handleDeleteConfirm = async () => {
    const response = await apiService.deleteVehicleServiceRecord(deleteId);
    if (response?.Result?.ResultCode === 0) {
      toast.success(response.Result?.ResultMessage || 'Kayıt başarıyla silindi', { position: "bottom-right" });
      closeDeleteConfirmation();
      fetchRecords(currentPage, perPage, searchText, sortColumn, sortDirection);
    } else {
      toast.error(response.Result?.ResultMessage || 'Kayıt silinirken bir hata oluştu', { position: "bottom-right" });
    }
  };

  const columns = [
    { name: 'Plaka', selector: row => row.LicensePlate, sortable: true },
    { name: 'Marka', selector: row => row.BrandName, sortable: true },
    { name: 'Model', selector: row => row.ModelName, sortable: true },
    { name: 'KM', selector: row => row.Mileage, sortable: true },
    { name: 'Model Yılı', selector: row => row.ModelYear, sortable: true },
    {
      name: 'Servis Tarihi',
      selector: row => row.ServiceDate,
      sortable: true,
      format: row => row.ServiceDate ? row.ServiceDate.split('T')[0] : '',
    },
    {
      name: 'Garanti',
      selector: row => row.HasWarranty,
      sortable: true,
      cell: row => (
        row.HasWarranty === true ? <span className="badge bg-success">Evet</span> :
        row.HasWarranty === false ? <span className="badge bg-danger">Hayır</span> :
        <span className="badge bg-secondary">-</span>
      ),
    },
    {
      name: 'Şehir',
      selector: row => row.ServiceCity?.CityName,
      sortable: true,
      format: row => row.ServiceCity?.CityName || '',
    },
    {
      name: 'Servis Notu',
      selector: row => row.ServiceNote,
      sortable: true,
      format: row => row.ServiceNote ? (row.ServiceNote.length > 50 ? row.ServiceNote.substring(0, 50) + '...' : row.ServiceNote) : '',
    },
    {
      name: 'İşlemler',
      cell: row => (
        <div className="btn-group" role="group">
          <Button variant="outline-primary" size="sm" onClick={() => openEditModal(row.Id)} title="Düzenle">
            <i className="fas fa-edit"></i>
          </Button>
          <Button variant="outline-danger" size="sm" onClick={() => openDeleteConfirmation(row.Id)} title="Sil">
            <i className="fas fa-trash"></i>
          </Button>
        </div>
      ),
      ignoreRowClick: true,
      allowOverflow: true,
      button: true,
    },
  ];

  const customStyles = {
    headCells: {
      style: {
        backgroundColor: '#343a40',
        color: '#fff',
        fontSize: '16px',
        fontWeight: 'bold',
      },
    },
  };

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-12">
          <div className="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 className="h2">Araç Servis Kayıtları</h1>
            <div className="btn-toolbar mb-2 mb-md-0">
              <div className="btn-group me-2">
                <Button variant="outline-secondary" size="sm" onClick={() => fetchRecords(currentPage, perPage, searchText, sortColumn, sortDirection)}>Yenile</Button>
                <Button variant="outline-secondary" size="sm" onClick={openCreateModal}>Ekle</Button>
              </div>
            </div>
          </div>

          <div className="card shadow-sm">
            <div className="card-body">
              <div className="mb-3">
                <input
                  type="text"
                  className="form-control"
                  placeholder="Ara..."
                  value={searchText}
                  onChange={handleSearchChange}
                />
              </div>
              <DataTable
                columns={columns}
                data={records}
                progressPending={loading}
                pagination
                paginationServer
                paginationTotalRows={totalRows}
                paginationPerPage={perPage}
                paginationRowsPerPageOptions={[10, 25, 50, 100]}
                onChangeRowsPerPage={handlePerRowsChange}
                onChangePage={handlePageChange}
                onSort={handleSort}
                sortServer
                striped
                highlightOnHover
                pointerOnHover
                customStyles={customStyles}
              />
            </div>
          </div>
        </div>
      </div>

      <VehicleServiceRecordFormModal
        key={modalKey}
        show={showFormModal}
        handleClose={closeFormModal}
        recordId={editRecordId}
        refreshRecords={() => fetchRecords(currentPage, perPage, searchText, sortColumn, sortDirection)}
      />

      <DeleteConfirmationModal
        show={showDeleteModal}
        handleClose={closeDeleteConfirmation}
        handleConfirm={handleDeleteConfirm}
      />
    </div>
  );
};

export default HomePage;
