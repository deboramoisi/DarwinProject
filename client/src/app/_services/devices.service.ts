import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Device } from '../_models/device';

@Injectable({
  providedIn: 'root'
})
export class DevicesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getDevices() {
    return this.http.get<Device[]>(this.baseUrl + 'devices');
  }

  getDevice(id: number) {
    return this.http.get<Device>(this.baseUrl + 'devices/' + id);
  }

  getDeviceTypes() {
    return this.http.get<string[]>(this.baseUrl + 'devices/device-types');
  }

  assignDevice(id: number) {
    return this.http.get(this.baseUrl + 'devices/assign-device/' + id);
  }

  unassignDevice(id: number) {
    return this.http.delete(this.baseUrl + 'devices/unassign-device/' + id);
  }

  addDevice(device: Device) {
    return this.http.post(this.baseUrl + 'admin/add-device', device);
  }

  updateDevice(device: Device) {
    return this.http.put(this.baseUrl + 'admin/edit-device', device);
  }

  deleteDevice(id: number) {
    return this.http.delete(this.baseUrl + 'admin/delete-device/' + id);
  }

}

