import axios from 'axios';
import type { BankStatementData, FileMetadata } from '../types';

const API_BASE_URL = 'https://localhost:7178/';

const api = axios.create({
  baseURL: API_BASE_URL,
});

export const fileApi = {
  // Загрузка файла
  uploadFile: async (file: File): Promise<BankStatementData> => {
    const formData = new FormData();
    formData.append('file', file);
    
    const response = await api.post('/Test', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  // Получение списка файлов
  getFiles: async (): Promise<FileMetadata[]> => {
    const response = await api.get('/Test');
    return response.data;
  },

  // Получение данных файла
  getFileData: async (fileId: number): Promise<BankStatementData> => {
    const response = await api.get(`/Test/${fileId}`);
    return response.data;
  },

  // Удаление файла
  deleteFile: async (fileId: number): Promise<void> => {
    await api.delete(`/Test/${fileId}`);
  },
};