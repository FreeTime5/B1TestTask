import React, { useState, useEffect } from "react";
import { Layout, Row, Col, message } from "antd";
import type { BankStatementData, FileMetadata } from "./types";
import { fileApi } from "./services/api";
import FileUploader from "./components/FileUploader";
import FileList from "./components/FileList";
import FileViewer from "./components/FileViewer";

const { Header, Content } = Layout;

const App: React.FC = () => {
  const [files, setFiles] = useState<FileMetadata[]>([]);
  const [selectedFileId, setSelectedFileId] = useState<number | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    loadFiles();
  }, []);

  const loadFiles = async () => {
    setLoading(true);
    try {
      const filesData = await fileApi.getFiles();
      setFiles(filesData);
    } catch (error) {
      message.error("Ошибка при загрузке списка файлов");
      console.error("Error loading files:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleFileUploaded = (data: BankStatementData) => {
    message.success("Файл успешно обработан");
    loadFiles();
    if (data.file.fileId) {
      setSelectedFileId(data.file.fileId);
    }
  };

  const handleViewFile = (fileId: number) => {
    setSelectedFileId(fileId);
  };

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <Header>
        <h1 style={{ color: "white", margin: 0 }}>Банковские отчеты</h1>
      </Header>

      <Content style={{ padding: "24px", background: "#f0f2f5" }}>
        <Row gutter={[24, 24]}>
          <Col xs={24} lg={8}>
            <FileUploader onFileUploaded={handleFileUploaded} />
            <FileList
              files={files}
              onViewFile={handleViewFile}
              loading={loading}
            />
          </Col>

          <Col xs={24} lg={16}>
            <FileViewer fileId={selectedFileId} />
          </Col>
        </Row>
      </Content>
    </Layout>
  );
};

export default App;
