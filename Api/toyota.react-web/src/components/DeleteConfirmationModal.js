import React from 'react';
import { Modal, Button } from 'react-bootstrap';

const DeleteConfirmationModal = ({ show, handleClose, handleConfirm }) => {
  return (
    <Modal show={show} onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Kayıt Sil</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        Bu kaydı silmek istediğinizden emin misiniz?
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>İptal</Button>
        <Button variant="danger" onClick={handleConfirm}>Sil</Button>
      </Modal.Footer>
    </Modal>
  );
};

export default DeleteConfirmationModal;

