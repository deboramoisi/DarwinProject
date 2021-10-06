import { Component, OnInit } from '@angular/core';
import { Device } from 'src/app/_models/device';
import { AccountService } from 'src/app/_services/account.service';
import { DevicesService } from 'src/app/_services/devices.service';
import Swal from 'sweetalert2/dist/sweetalert2.js'; 

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {
  devices: Device[];

  constructor(private devicesService: DevicesService, public accountService: AccountService) { }

  ngOnInit(): void {
    this.loadDevices();
  }

  loadDevices() {
    this.devicesService.getDevices().subscribe(devices => {
      this.devices = devices;
    });
  }

  toggleAssignationDevice(event) {
    if (event.target.checked) {
      this.devicesService.assignDevice(event.target.value).subscribe();
    } else {
      console.log('unchecked');
    }
  }

  deleteDevice(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete the device?',
      text: 'You will not be able to recover it!',
      icon: 'warning',
      showCancelButton: true
    }).then((result) => {
      if (result.value) {
        this.devicesService.deleteDevice(id).subscribe(() => {
          this.devices = this.devices.filter(x => x.id != id);
        })        
      }
    })    
  }

}
