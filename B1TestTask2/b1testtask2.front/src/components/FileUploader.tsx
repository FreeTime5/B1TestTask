import React, { useState } from "react";
import { Upload, Button, message, Card } from "antd";
import { UploadOutlined } from "@ant-design/icons";
import { fileApi } from "../services/api";
import type { BankStatementData } from "../types";

interface FileUploaderProps {
  onFileUploaded: (data: BankStatementData) => void;
}

const FileUploader: React.FC<FileUploaderProps> = ({ onFileUploaded }) => {
  const [loading, setLoading] = useState(false);

  const handleUpload = async (file: File) => {
    setLoading(true);
    try {
      const data = await fileApi.uploadFile(file);
      onFileUploaded(data);
      message.success("Файл успешно загружен");
    } catch (error) {
      message.error("Ошибка при загрузке файла");
      console.error("Upload error:", error);
    } finally {
      setLoading(false);
    }
    return false;
  };

  return (
    <Card title="Загрузка Excel файла" style={{ marginBottom: 20 }}>
      <Upload
        accept=".xlsx,.xls"
        beforeUpload={handleUpload}
        showUploadList={false}
      >
        <Button icon={<UploadOutlined />} loading={loading} size="large">
          Выберите Excel файл
        </Button>
      </Upload>
      <div style={{ marginTop: 10, color: "#666", fontSize: 12 }}>
        Поддерживаемые форматы: .xlsx, .xls
      </div>
    </Card>
  );
};

export default FileUploader;
