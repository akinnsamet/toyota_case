import React, { useState, useEffect, useCallback } from 'react';
import DataTable from 'react-data-table-component';
import { Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import apiService from '../services/apiService';
import LogDetailModal from '../components/LogDetailModal';

const ApplicationLogsPage = () => {
  const [logs, setLogs] = useState([]);
  const [loading, setLoading] = useState(false);
  const [totalRows, setTotalRows] = useState(0);
  const [perPage, setPerPage] = useState(10);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchText, setSearchText] = useState('');

  const [showLogDetailModal, setShowLogDetailModal] = useState(false);
  const [selectedLogContent, setSelectedLogContent] = useState(null);

  const fetchLogs = useCallback(async (page, size, search) => {
    setLoading(true);
    const request = {
      Draw: page,
      SortingPaging: {
        SortItem: {
          ColumnName: 'CreateDate',
          ColumnOrder: 'Descending',
        },
        PageNumber: page,
        NumberRecords: size,
      },
      SearchText: search || '',
    };

    const response = await apiService.getApplicationLogs(request);
    if (response?.Result?.ResultCode === 0) {
      const formattedLogs = (response.ReturnObject || []).map((log, index) => ({ id: index, logEntry: log }));
      setLogs(formattedLogs);
      setTotalRows(response.TotalCount || 0);
    } else {
      toast.error(response?.Result?.ResultMessage || 'Uygulama logları yüklenirken bir hata oluştu', { position: "bottom-right" });
    }
    setLoading(false);
  }, []);

  useEffect(() => {
    fetchLogs(currentPage, perPage, searchText);
  }, [fetchLogs, currentPage, perPage, searchText]);

  const handlePageChange = (page) => {
    setCurrentPage(page);
  };

  const handlePerRowsChange = (newPerPage, page) => {
    setPerPage(newPerPage);
    setCurrentPage(page);
  };

  const handleSearchChange = (e) => {
    setSearchText(e.target.value);
    setCurrentPage(1);
  };

  const handleRowClicked = (row) => {
    try {
      const parsedLog = JSON.parse(row.logEntry);
      setSelectedLogContent(parsedLog);
    } catch (e) {
      setSelectedLogContent(row.logEntry);
    }
    setShowLogDetailModal(true);
  };

  const closeLogDetailModal = () => {
    setShowLogDetailModal(false);
    setSelectedLogContent(null);
  };

  const columns = [
    { name: 'Log Kaydı', selector: row => row.logEntry, grow: 4 },
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
    cells: {
      style: {
        wordBreak: 'break-all',
      },
    },
  };

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-12">
          <div className="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 className="h2">Uygulama Logları</h1>
            <div className="btn-toolbar mb-2 mb-md-0">
              <div className="btn-group me-2">
                <Button variant="outline-secondary" size="sm" onClick={() => fetchLogs(currentPage, perPage, searchText)}>Yenile</Button>
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
                data={logs}
                progressPending={loading}
                pagination
                paginationServer
                paginationTotalRows={totalRows}
                paginationPerPage={perPage}
                paginationRowsPerPageOptions={[10, 25, 50, 100]}
                onChangeRowsPerPage={handlePerRowsChange}
                onChangePage={handlePageChange}
                striped
                highlightOnHover
                pointerOnHover
                customStyles={customStyles}
                onRowClicked={handleRowClicked}
              />
            </div>
          </div>
        </div>
      </div>

      <LogDetailModal
        show={showLogDetailModal}
        handleClose={closeLogDetailModal}
        logContent={selectedLogContent}
      />
    </div>
  );
};

export default ApplicationLogsPage;
