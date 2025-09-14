import React from "react";
import { List, Card, Button, Space, Typography } from "antd";
import { EyeOutlined } from "@ant-design/icons";
import type { FileMetadata } from "../types";

const { Text } = Typography;

interface FileListProps {
  files: FileMetadata[];
  onViewFile: (fileId: number) => void;
  loading?: boolean;
}

const FileList: React.FC<FileListProps> = ({
  files,
  onViewFile,
  loading = false,
}) => {
  return (
    <Card title="Загруженные файлы" loading={loading}>
      <List
        dataSource={files}
        renderItem={(file) => (
          <List.Item
            actions={[
              <Button
                key="view"
                icon={<EyeOutlined />}
                onClick={() => onViewFile(file.fileId)}
              >
                Просмотр
              </Button>,
            ]}
          >
            <List.Item.Meta
              title={
                <Space>
                  <Text strong>{file.fileName}</Text>
                </Space>
              }
              description={
                <Space direction="vertical" size={0}>
                  <Text type="secondary">Банк: {file.bankName}</Text>
                  <Text type="secondary">
                    Период: {file.periodStart} - {file.periodEnd}
                  </Text>
                  <Text type="secondary">Загружен: {file.uploadDate}</Text>
                </Space>
              }
            />
          </List.Item>
        )}
      />
    </Card>
  );
};

export default FileList;
