import React, { useState, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import apiService from '../services/apiService';
import { toast } from 'react-toastify';

const VehicleServiceRecordFormModal = ({ show, handleClose, recordId, refreshRecords }) => {
  const [record, setRecord] = useState({
    id: 0,
    licensePlate: '',
    brandName: '',
    modelName: '',
    mileage: '',
    modelYear: '',
    serviceDate: '',
    hasWarranty: '',
    serviceCityId: '',
    serviceNote: '',
  });
  const [cities, setCities] = useState([]);

  useEffect(() => {
    const fetchCities = async () => {
      const response = await apiService.getCities();
      if (response?.Result?.ResultCode === 0) {
        setCities(response.ReturnObject || []);
      } else {
        toast.error(response?.Result?.ResultMessage || 'Şehirler yüklenirken bir hata oluştu', { position: "bottom-right" });
      }
    };
    fetchCities();
  }, []);

  useEffect(() => {
    const fetchRecord = async () => {
      if (recordId) {
        const response = await apiService.getVehicleServiceRecord(recordId);
        if (response?.Result?.ResultCode === 0) {
          const fetchedRecord = response.ReturnObject;
          setRecord({
            id: fetchedRecord.Id,
            licensePlate: fetchedRecord.LicensePlate,
            brandName: fetchedRecord.BrandName,
            modelName: fetchedRecord.ModelName,
            mileage: fetchedRecord.Mileage,
            modelYear: fetchedRecord.ModelYear,
            serviceDate: fetchedRecord.ServiceDate ? fetchedRecord.ServiceDate.split('T')[0] : '',
            hasWarranty: fetchedRecord.HasWarranty != null ? fetchedRecord.HasWarranty.toString() : '',
            serviceCityId: fetchedRecord.ServiceCity?.Id || '',
            serviceNote: fetchedRecord.ServiceNote,
          });
        } else {
          toast.error(response?.Result?.ResultMessage || 'Kayıt getirilirken bir hata oluştu', { position: "bottom-right" });
        }
      } else {
        setRecord({
          id: 0,
          licensePlate: '',
          brandName: '',
          modelName: '',
          mileage: '',
          modelYear: '',
          serviceDate: '',
          hasWarranty: '',
          serviceCityId: '',
          serviceNote: '',
        });
      }
    };
    fetchRecord();
  }, [recordId]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setRecord((prevRecord) => ({
      ...prevRecord,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let response;

    const formattedRecord = {
      Id: record.id || 0,
      LicensePlate: record.licensePlate,
      BrandName: record.brandName,
      ModelName: record.modelName,
      Mileage: parseInt(record.mileage || '0'),
      ModelYear: parseInt(record.modelYear || '0'),
      ServiceDate: record.serviceDate,
      HasWarranty: record.hasWarranty === 'true' ? true : (record.hasWarranty === 'false' ? false : null),
      ServiceCityId: record.serviceCityId ? parseInt(record.serviceCityId) : null, // Burası düzeltildi
      ServiceNote: record.serviceNote,
    };

    if (formattedRecord.Id) {
      response = await apiService.updateVehicleServiceRecord(formattedRecord);
    } else {
      response = await apiService.createVehicleServiceRecord(formattedRecord);
    }

    if (response?.Result?.ResultCode === 0) {
      toast.success(response.Result?.ResultMessage || 'İşlem başarılı', { position: "bottom-right" });
      handleClose();
      refreshRecords();
    } else {
      toast.error(response.Result?.ResultMessage || 'İşlem sırasında bir hata oluştu', { position: "bottom-right" });
    }
  };

  return (
    <Modal show={show} onHide={() => {
      handleClose();
      setRecord({
        id: 0,
        licensePlate: '',
        brandName: '',
        modelName: '',
        mileage: '',
        modelYear: '',
        serviceDate: '',
        hasWarranty: '',
        serviceCityId: '',
        serviceNote: '',
      });
    }} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>{record.Id ? 'Araç Servis Kaydını Düzenle' : 'Yeni Araç Servis Kaydı'}</Modal.Title>
      </Modal.Header>
      <Form onSubmit={handleSubmit}>
        <Modal.Body>
          <input type="hidden" name="Id" value={record.Id} />

          <div className="row">
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="licensePlate">Araç Plakası *</Form.Label>
              <Form.Control
                type="text"
                id="licensePlate"
                name="licensePlate"
                value={record.licensePlate}
                onChange={handleChange}
                required
              />
            </Form.Group>
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="brandName">Marka Adı *</Form.Label>
              <Form.Control
                type="text"
                id="brandName"
                name="brandName"
                value={record.brandName}
                onChange={handleChange}
                required
              />
            </Form.Group>
          </div>

          <div className="row">
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="modelName">Model Adı *</Form.Label>
              <Form.Control
                type="text"
                id="modelName"
                name="modelName"
                value={record.modelName}
                onChange={handleChange}
                required
              />
            </Form.Group>
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="mileage">KM Bilgisi *</Form.Label>
              <Form.Control
                type="number"
                id="mileage"
                name="mileage"
                value={record.mileage}
                onChange={handleChange}
                required
              />
            </Form.Group>
          </div>

          <div className="row">
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="modelYear">Model Yılı</Form.Label>
              <Form.Control
                type="number"
                id="modelYear"
                name="modelYear"
                value={record.modelYear}
                onChange={handleChange}
              />
            </Form.Group>
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="serviceDate">Servise Geliş Tarihi *</Form.Label>
              <Form.Control
                type="date"
                id="serviceDate"
                name="serviceDate"
                value={record.serviceDate}
                onChange={handleChange}
                required
              />
            </Form.Group>
          </div>

          <div className="row">
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="hasWarranty">Garantisi Var mı?</Form.Label>
              <Form.Select id="hasWarranty" name="hasWarranty" value={record.hasWarranty} onChange={handleChange}>
                <option value="">Seçiniz</option>
                <option value="true">Evet</option>
                <option value="false">Hayır</option>
              </Form.Select>
            </Form.Group>
            <Form.Group className="col-md-6 mb-3">
              <Form.Label htmlFor="serviceCityId">Servis Şehri</Form.Label>
              <Form.Select id="serviceCityId" name="serviceCityId" value={record.serviceCityId} onChange={handleChange}>
                <option value="">Şehir Seçiniz</option>
                {cities.map((city) => (
                  <option key={city.Id} value={city.Id}>
                    {city.CityName}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
          </div>

          <Form.Group className="mb-3">
            <Form.Label htmlFor="serviceNote">Servis Notu</Form.Label>
            <Form.Control
              as="textarea"
              id="serviceNote"
              name="serviceNote"
              rows="3"
              value={record.serviceNote}
              onChange={handleChange}
            />
          </Form.Group>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>İptal</Button>
          <Button variant="primary" type="submit">Kaydet</Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
};

export default VehicleServiceRecordFormModal;
