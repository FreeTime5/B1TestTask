import React, { useState, useEffect } from "react";
import { Card, Tabs, Typography, Spin, Alert } from "antd";
import type { BankStatementData } from "../types";
import VirtualDataTable from "./VirtualDataTable";
import { fileApi } from "../services/api";

const { Title, Text } = Typography;
const { TabPane } = Tabs;

interface FileViewerProps {
  fileId: number | null;
}

const FileViewer: React.FC<FileViewerProps> = ({ fileId }) => {
  const [data, setData] = useState<BankStatementData | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (fileId) {
      loadFileData(fileId);
    } else {
      setData(null);
    }
  }, [fileId]);

  const loadFileData = async (id: number) => {
    setLoading(true);
    setError(null);
    try {
      const fileData = await fileApi.getFileData(id);
      setData(fileData);
    } catch (err) {
      setError("Ошибка при загрузке данных файла");
      console.error("Error loading file data:", err);
    } finally {
      setLoading(false);
    }
  };

  if (!fileId) {
    return (
      <Card>
        <Text type="secondary">Выберите файл для просмотра</Text>
      </Card>
    );
  }

  if (loading) {
    return (
      <Card>
        <Spin size="large" />
        <Text>Загрузка данных...</Text>
      </Card>
    );
  }

  if (error) {
    return (
      <Card>
        <Alert message={error} type="error" />
      </Card>
    );
  }

  if (!data) {
    return null;
  }

  return (
    <Card>
      <Title level={3}>{data.file.fileName}</Title>

      <div style={{ marginBottom: 16 }}>
        <Text strong>Описание: </Text>
        <Text>{data.file.reportTitle}</Text>
        <br />
        <Text strong>Банк: </Text>
        <Text>{data.file.bankName}</Text>
        <br />
        <Text strong>Период: </Text>
        <Text>
          {" "}
          {data.file.periodStart} - {data.file.periodEnd}
        </Text>
        <br />
        <Text strong>Валюта: </Text>
        <Text>{data.file.currency}</Text>
        <br />
        <Text strong>Записей: </Text>
        <Text>{data.records.length}</Text>
      </div>

      <Tabs defaultActiveKey="accounts">
        <TabPane tab="Счета" key="accounts">
          <VirtualDataTable data={data.records} title="Балансовые счета" />
        </TabPane>

        <TabPane tab="Итоги по классам" key="totals">
          <VirtualDataTable
            data={data.classTotals.map((t) => ({
              bankAccount: t.isSubclass
                ? t.subclass
                : `${t.classId}. ${t.className}`,
              activeOpeningBalance: t.activeOpeningBalance,
              pasiveOpeningBalance: t.pasiveOpeningBalance,
              debitTurnover: t.debitTurnover,
              creditTurnover: t.creditTurnover,
              activeOutgoingBalance: t.activeOutgoingBalance,
              passiveOutgoingBalance: t.passiveOutgoingBalance,
              classId: t.classId,
              className: t.className,
            }))}
            title="Итоги по классам"
          />
        </TabPane>
      </Tabs>
    </Card>
  );
};

export default FileViewer;
