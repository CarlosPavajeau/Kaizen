import { Injectable } from '@angular/core';
import { HttpClient, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UPLOAD_DOWNLOAD_API_URL } from '@app/global/endpoints';
import { FileResponse } from '../models/file-response';

@Injectable({
	providedIn: 'root'
})
export class UploadDownloadService {
	constructor(private http: HttpClient) {}

	uploadFiles(files: File[]) {
		const formData: FormData = new FormData();
		Array.from(files).map((file, index) => {
			return formData.append('file' + index, file, file.name);
		});
		return this.http.post<FileResponse[]>(UPLOAD_DOWNLOAD_API_URL, formData, {
			reportProgress: true,
			observe: 'events'
		});
	}

	downloadFile(fileName: string, downloadName?: string): Observable<Blob> {
		return this.http.get<Blob>(
			`${UPLOAD_DOWNLOAD_API_URL}?fileName=${fileName}&downloadName=${
				downloadName ? downloadName :
				fileName}`,
			{ reportProgress: true, responseType: 'blob' as 'json' }
		);
	}
}
