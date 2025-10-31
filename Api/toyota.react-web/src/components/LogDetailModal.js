import React from 'react';
import { Modal, Button } from 'react-bootstrap';

const LogDetailModal = ({ show, handleClose, logContent }) => {
  return (
    <Modal show={show} onHide={handleClose} size="lg">
      <Modal.Header closeButton>
        <Modal.Title>Log Detayları</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <pre style={{ whiteSpace: 'pre-wrap', wordBreak: 'break-all' }}>
          {JSON.stringify(logContent, null, 2)}
        </pre>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>Kapat</Button>
      </Modal.Footer>
    </Modal>
  );
};

export default LogDetailModal;

