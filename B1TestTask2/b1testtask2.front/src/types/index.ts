// types/index.ts
export interface BankAccountRecord {
  classId: string;
  className: string;
  bankAccount: string;
  activeOpeningBalance: number | null;
  pasiveOpeningBalance: number | null;
  debitTurnover: number | null;
  creditTurnover: number | null;
  activeOutgoingBalance: number | null;
  passiveOutgoingBalance: number | null;
}

export interface ClassTotalRecord {
  classId: string;
  className: string;
  activeOpeningBalance: number | null;
  pasiveOpeningBalance: number | null;
  debitTurnover: number | null;
  creditTurnover: number | null;
  activeOutgoingBalance: number | null;
  passiveOutgoingBalance: number | null;
  isSubclass: boolean | null,
  subclass: string | null
}

export interface FileMetadata {
  fileId: number;
  fileName: string;
  bankName: string;
  periodStart: string;
  periodEnd: string;
  uploadDate: string;
  currency: string;
  reportTitle: string;
}

export interface BankStatementData {
  file: FileMetadata;
  records: BankAccountRecord[];
  classTotals: ClassTotalRecord[];
}