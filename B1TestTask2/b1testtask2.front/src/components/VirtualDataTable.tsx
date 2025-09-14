import React, { useRef } from "react";
import { Table, Card, Typography } from "antd";
import type { BankAccountRecord } from "../types";

const { Title } = Typography;

interface VirtualDataTableProps {
  data: BankAccountRecord[];
  loading?: boolean;
  title?: string;
}

const VirtualDataTable: React.FC<VirtualDataTableProps> = ({
  data,
  loading = false,
  title = "Данные счетов",
}) => {
  const tableRef = useRef(null);

  const columns = [
    {
      title: "Код счета",
      dataIndex: "bankAccount",
      key: "bankAccount",
      width: 100,
      fixed: "left" as const,
    },
    {
      title: "Вх. сальдо (Актив)",
      dataIndex: "activeOpeningBalance",
      key: "activeOpeningBalance",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
    {
      title: "Вх. сальдо (Пассив)",
      dataIndex: "pasiveOpeningBalance",
      key: "pasiveOpeningBalance",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
    {
      title: "Оборот по дебету",
      dataIndex: "debitTurnover",
      key: "debitTurnover",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
    {
      title: "Оборот по кредиту",
      dataIndex: "creditTurnover",
      key: "creditTurnover",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
    {
      title: "Исх. сальдо (Актив)",
      dataIndex: "activeOutgoingBalance",
      key: "activeOutgoingBalance",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
    {
      title: "Исх. сальдо (Пассив)",
      dataIndex: "passiveOutgoingBalance",
      key: "passiveOutgoingBalance",
      width: 150,
      render: (value: number | null) => value?.toLocaleString("ru-RU") || "0",
    },
  ];

  return (
    <Card>
      <Title level={4}>{title}</Title>
      <Table
        ref={tableRef}
        columns={columns}
        dataSource={data.map((item, index) => ({ ...item, key: index }))}
        loading={loading}
        scroll={{ x: 1000, y: 600 }}
        pagination={false}
        size="small"
        virtual
        style={{
          height: "600px",
          overflow: "auto",
        }}
      />
    </Card>
  );
};

export default VirtualDataTable;
