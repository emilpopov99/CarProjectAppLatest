using CarProjectApp.Models;
using CarProjectApp.Repositories;
using CarProjectApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProjectApp
{
    public partial class Form1 : Form
    {
        private readonly ICarExtraRepository _carextraRepository;
        private readonly IEmissionRepository _emissionRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly IVinNumberRepository _vinRepository;
        private readonly ICarRepository _carRepository;
        private readonly IExtraRepository _extraRepository;
        public Form1()
        {
            InitializeComponent();
            ApplicationDbContext _context = new ApplicationDbContext();
            _context.ChangeTracker.AutoDetectChangesEnabled = false;
            _ownerRepository = new OwnerRepository(_context);
            _vinRepository = new VinNumberRepository(_context);
            _carRepository = new CarRepository(_context);
            _extraRepository = new ExtraRepository(_context);
            _emissionRepository = new EmissionRepository(_context);
            _carextraRepository = new CarExtraRepository(_context);
            String[] emissions = _emissionRepository.GetAllEmissions.Select(em=>em.EuroStandart).ToArray();
            CbEmission.Items.AddRange(emissions);
            Onstart();
            GetStart();
        }
        
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBrand_Validating(sender, new CancelEventArgs());
                TxtModel_Validating(sender, new CancelEventArgs());
                NudCubic_Validating(sender, new CancelEventArgs());
                NudHp_Validating(sender, new CancelEventArgs());
                TxtBodyType_Validating(sender, new CancelEventArgs());
                CbEmission_Validating(sender, new CancelEventArgs());
                TxtFuel_Validating(sender, new CancelEventArgs());
                TxtVin_Validating(sender, new CancelEventArgs());
                TxtRegNum_Validating(sender, new CancelEventArgs());
                TxtOwner_Validating(sender, new CancelEventArgs());
                TxtEgn_Validating(sender, new CancelEventArgs());
            }
            catch (ArgumentException)
            {
                return;
            }
            
            var owner = new Owner()
                {
                    Name = TxtOwner.Text,
                    Egn = TxtEgn.Text
                };
                _ownerRepository.AddOwner(owner);
                var vin = new VinNumber()
                {
                    Vin = TxtVin.Text
                };
                _vinRepository.AddVinNumber(vin);
                int EmId = _emissionRepository.GetAllEmissions.FirstOrDefault(em => em.EuroStandart == CbEmission.Text).Id;
                var car = new Car()
                {
                    Brand = TxtBrand.Text,
                    Model = TxtModel.Text,
                    ManifactoryDate = DtManfDate.Value,
                    CubicCapacity = (int)NudCubic.Value,
                    Horsepower = (int)NudHp.Value,
                    BodyType = TxtBodyType.Text,
                    FuelType = TxtFuel.Text,
                    RegNumber = TxtRegNum.Text,
                    EmissionId = EmId,
                    OwnerId = owner.Id,
                    VinNumberId = vin.Id

                };
                _carRepository.AddCar(car);

                if (CheckBoxAC.Checked)
                {
                    int extraId = _extraRepository.GetAllExtras.FirstOrDefault(x => x.Name == "A/C").Id;
                    CarExtra carExtra = new CarExtra();
                    carExtra.CarId = car.Id;
                    carExtra.ExtraId = extraId;
                    _carextraRepository.AddCarExtra(carExtra);
                }
                if (CheckBoxElPcg.Checked)
                {
                    int extraId = _extraRepository.GetAllExtras.FirstOrDefault(x => x.Name == "El.Pack").Id;
                    CarExtra carExtra = new CarExtra();
                    carExtra.CarId = car.Id;
                    carExtra.ExtraId = extraId;
                    _carextraRepository.AddCarExtra(carExtra);
                    
                }
                if (CheckBoxLAW.Checked)
                {
                    int extraId = _extraRepository.GetAllExtras.FirstOrDefault(x => x.Name == "Light Alloy Wheels").Id;
                    CarExtra carExtra = new CarExtra();
                    carExtra.CarId = car.Id;
                    carExtra.ExtraId = extraId;
                    _carextraRepository.AddCarExtra(carExtra);
                }

            ClearForm();
            GetStart();

        }
        private void Onstart()
        {
            CarGridView.ColumnCount = 14;
            CarGridView.Columns[0].Name = "Id";
            CarGridView.Columns[1].Name = "Марка";
            CarGridView.Columns[2].Name = "Модел";
            CarGridView.Columns[3].Name = "Произведен на";
            CarGridView.Columns[4].Name = "Капацитет(cm3)";
            CarGridView.Columns[5].Name = "Мощност(к.с)";
            CarGridView.Columns[6].Name = "Тип на купето";
            CarGridView.Columns[7].Name = "Евро Стандарт";
            CarGridView.Columns[8].Name = "Тип на горивото";
            CarGridView.Columns[9].Name = "Vin номер";
            CarGridView.Columns[10].Name = "Рег.номер";
            CarGridView.Columns[11].Name = "Име(собственик)";
            CarGridView.Columns[12].Name = "ЕГН(собственик)";
            CarGridView.Columns[13].Name = "Екстри";
        }
        
        private void GetStart()
        {
            CarGridView.Rows.Clear();
            var carModels = _carRepository.GetAllCars;
            foreach(var car in carModels)
            {
                var extrasIDs=_carextraRepository.GetAllCarExtras.Where(c => c.CarId == car.Id).Select(ex=>ex.ExtraId);
                List<String> Extras = new List<string>();
                foreach(var extraID in extrasIDs)
                {
                    Extras.Add(_extraRepository.GetAllExtras.FirstOrDefault(ex => ex.Id == extraID).Name);
                }
                string extras = string.Join(',', Extras);
                string[] currentRow = {
                    car.Id.ToString(),
                    car.Brand,
                    car.Model,
                    car.ManifactoryDate.ToShortDateString(),
                    car.CubicCapacity.ToString(),
                    car.Horsepower.ToString(),
                    car.BodyType,
                    _emissionRepository.GetAllEmissions.FirstOrDefault(em=>em.Id==car.EmissionId).EuroStandart,
                    car.FuelType,
                    _vinRepository.GetAllVinNumbers.FirstOrDefault(em=>em.Id==car.VinNumberId).Vin,
                    car.RegNumber,
                    _ownerRepository.GetAllOwners.FirstOrDefault(em=>em.Id==car.OwnerId).Name,
                    _ownerRepository.GetAllOwners.FirstOrDefault(em=>em.Id==car.OwnerId).Egn,
                    extras
                };
                CarGridView.Rows.Add(currentRow);
            }
        }
        private void ClearForm()
        {
            TxtBrand.Text = "";
            TxtModel.Text = "";
            NudCubic.Value = 0;
            NudHp.Value = 0;
            TxtBodyType.Text = "";
            CbEmission.Text = "";
            TxtFuel.Text = "";
            TxtVin.Text = "";
            TxtRegNum.Text = "";
            TxtOwner.Text = "";
            TxtEgn.Text = "";
            CheckBoxAC.Checked = false;
            CheckBoxElPcg.Checked = false;
            CheckBoxLAW.Checked = false;
            ErrorTxtBrand.SetError(TxtBrand, "");
            ErrorTxtModel.SetError(TxtModel, "");
            ErrorNudCubic.SetError(NudCubic, "");
            ErrorNudHp.SetError(NudHp, "");
            ErrorTxtBodyType.SetError(TxtBodyType, "");
            ErrorCbEmission.SetError(CbEmission, "");
            ErrorTxtFuel.SetError(TxtFuel, "");
            ErrorTxtVin.SetError(TxtVin, "");
            ErrorTxtRegNum.SetError(TxtRegNum, "");
            ErrorTxtOwner.SetError(TxtOwner, "");
            ErrorTxtBrand.SetError(TxtBrand, "");
            ErrorTxtEgn.SetError(TxtEgn, "");
            BtnAdd.Enabled = true;
            BtnUpdate.Enabled = false;
            BtnDelete.Enabled = false;


        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBrand_Validating(sender, new CancelEventArgs());
                TxtModel_Validating(sender, new CancelEventArgs());
                NudCubic_Validating(sender, new CancelEventArgs());
                NudHp_Validating(sender, new CancelEventArgs());
                TxtBodyType_Validating(sender, new CancelEventArgs());
                CbEmission_Validating(sender, new CancelEventArgs());
                TxtFuel_Validating(sender, new CancelEventArgs());
                TxtVin_Validating(sender, new CancelEventArgs());
                TxtRegNum_Validating(sender, new CancelEventArgs());
                TxtOwner_Validating(sender, new CancelEventArgs());
                TxtEgn_Validating(sender, new CancelEventArgs());
            }
            catch (ArgumentException)
            {
                return;
            }
            
            var carToDelete = _carRepository.GetAllCars.FirstOrDefault(c => c.Id == Int32.Parse(TxtId.Text));
            _carRepository.DeleteCar(carToDelete);
            ClearForm();
            GetStart();
        }

        private void CarGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            int rowIndex = e.RowIndex;
            TxtId.Text = CarGridView.Rows[rowIndex].Cells[0].Value.ToString();
            TxtBrand.Text = CarGridView.Rows[rowIndex].Cells[1].Value.ToString();
            TxtModel.Text = CarGridView.Rows[rowIndex].Cells[2].Value.ToString();
            DtManfDate.Value = DateTime.Parse(CarGridView.Rows[rowIndex].Cells[3].Value.ToString());
            NudCubic.Value = Int32.Parse(CarGridView.Rows[rowIndex].Cells[4].Value.ToString());
            NudHp.Value = Int32.Parse(CarGridView.Rows[rowIndex].Cells[5].Value.ToString());
            TxtBodyType.Text = CarGridView.Rows[rowIndex].Cells[6].Value.ToString();
            CbEmission.SelectedItem = CarGridView.Rows[rowIndex].Cells[7].Value.ToString();
            TxtFuel.Text = CarGridView.Rows[rowIndex].Cells[8].Value.ToString();
            TxtVin.Text = CarGridView.Rows[rowIndex].Cells[9].Value.ToString();
            TxtRegNum.Text = CarGridView.Rows[rowIndex].Cells[10].Value.ToString();
            TxtOwner.Text = CarGridView.Rows[rowIndex].Cells[11].Value.ToString();
            TxtEgn.Text = CarGridView.Rows[rowIndex].Cells[12].Value.ToString();
            ErrorTxtBrand.SetError(TxtBrand, "");
            ErrorTxtModel.SetError(TxtModel, "");
            ErrorNudCubic.SetError(NudCubic, "");
            ErrorNudHp.SetError(NudHp, "");
            ErrorTxtBodyType.SetError(TxtBodyType, "");
            ErrorCbEmission.SetError(CbEmission, "");
            ErrorTxtFuel.SetError(TxtFuel, "");
            ErrorTxtVin.SetError(TxtVin, "");
            ErrorTxtRegNum.SetError(TxtRegNum, "");
            ErrorTxtOwner.SetError(TxtOwner, "");
            ErrorTxtBrand.SetError(TxtBrand, "");
            ErrorTxtEgn.SetError(TxtEgn, "");
            BtnAdd.Enabled = false;
            BtnUpdate.Enabled = true;
            BtnDelete.Enabled = true;

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBrand_Validating(sender, new CancelEventArgs());
                TxtModel_Validating(sender, new CancelEventArgs());
                NudCubic_Validating(sender, new CancelEventArgs());
                NudHp_Validating(sender, new CancelEventArgs());
                TxtBodyType_Validating(sender, new CancelEventArgs());
                CbEmission_Validating(sender, new CancelEventArgs());
                TxtFuel_Validating(sender, new CancelEventArgs());
                TxtVin_Validating(sender, new CancelEventArgs());
                TxtRegNum_Validating(sender, new CancelEventArgs());
                TxtOwner_Validating(sender, new CancelEventArgs());
                TxtEgn_Validating(sender, new CancelEventArgs());
            }
            catch (ArgumentException)
            {
                return;
            }
            
            var carToUpdate = new Car();
            carToUpdate = _carRepository.GetAllCars.FirstOrDefault(c => c.Id == Int32.Parse(TxtId.Text));
            carToUpdate.Brand = TxtBrand.Text;
            carToUpdate.Model = TxtModel.Text;
            carToUpdate.ManifactoryDate = DtManfDate.Value;
            carToUpdate.CubicCapacity = Convert.ToInt32(NudCubic.Value);
            carToUpdate.Horsepower = Convert.ToInt32(NudHp.Value);
            carToUpdate.BodyType = TxtBodyType.Text;
            carToUpdate.EmissionId = _emissionRepository.GetAllEmissions.FirstOrDefault(em=>em.EuroStandart==CbEmission.SelectedItem.ToString()).Id;
            carToUpdate.FuelType = TxtFuel.Text;
            var OldVin = _vinRepository.GetAllVinNumbers.FirstOrDefault(vn => vn.Id == carToUpdate.VinNumberId);

            if (OldVin.Vin != TxtVin.Text)
            {
                OldVin.Vin = TxtVin.Text;
                _vinRepository.UpdateVinNumber(OldVin);
            }
            var OldOwner = _ownerRepository.GetAllOwners.FirstOrDefault(ow => ow.Id == carToUpdate.OwnerId);
            if (OldOwner.Name != TxtOwner.Text)
            {
                var ExistingOwner = _ownerRepository.GetAllOwners.FirstOrDefault(ow => ow.Name == TxtOwner.Text);
                if (ExistingOwner != null)
                {
                    carToUpdate.OwnerId = ExistingOwner.Id;
                }
                else
                {
                    var NewOwner = new Owner();
                    NewOwner.Name = TxtOwner.Text;
                    NewOwner.Egn = TxtEgn.Text;
                    _ownerRepository.AddOwner(NewOwner);
                    carToUpdate.OwnerId = NewOwner.Id;
                }
                
            }
            else
            {
                if (OldOwner.Egn != TxtEgn.Text)
                {
                    OldOwner.Egn = TxtEgn.Text;
                    _ownerRepository.UpdateOwner(OldOwner);
                }
            }
            carToUpdate.RegNumber = TxtRegNum.Text;
           _carRepository.UpdateCar(carToUpdate);
            ClearForm();
            GetStart();
        }

        private void TxtBrand_Validating(object sender, CancelEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(TxtBrand.Text))
            {
                e.Cancel = true;
                ErrorTxtBrand.SetError(TxtBrand, "Моля, въведете Марка на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtBrand.SetError(TxtBrand, "");
            }
        }

        private void TxtModel_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtModel.Text))
            {
                e.Cancel = true;
                ErrorTxtModel.SetError(TxtModel, "Моля, въведете Модел на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtModel.SetError(TxtModel, "");
            }

        }

        private void NudCubic_Validating(object sender, CancelEventArgs e)
        {
            if (NudCubic.Value==0)
            {
                e.Cancel = true;
                ErrorNudCubic.SetError(NudCubic, "Моля, въведете Капацитет(в cm3) на двигателя!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorNudCubic.SetError(NudCubic, "");
            }

        }

        private void NudHp_Validating(object sender, CancelEventArgs e)
        {
            if (NudHp.Value == 0)
            {
                e.Cancel = true;
                ErrorNudHp.SetError(NudHp, "Моля, въведете Мощност(в конски сили) на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorNudHp.SetError(NudHp, "");
            }

        }

        private void TxtBodyType_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtBodyType.Text))
            {
                e.Cancel = true;
                ErrorTxtBodyType.SetError(TxtBodyType, "Моля, въведете тип на купето на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtBodyType.SetError(TxtBodyType, "");
            }

        }

        private void CbEmission_Validating(object sender, CancelEventArgs e)
        {   //CbEmission.SelectedValue
            if (string.IsNullOrWhiteSpace(CbEmission.Text))
            {
                e.Cancel = true;
                ErrorCbEmission.SetError(CbEmission, "Моля, изберете Евро категория на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorCbEmission.SetError(CbEmission, "");
            }

        }

        private void TxtFuel_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFuel.Text))
            {
                e.Cancel = true;
                ErrorTxtFuel.SetError(TxtFuel, "Моля, въведете Тип на горивото за автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtFuel.SetError(TxtFuel, "");
            }
        }

        private void TxtVin_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtVin.Text))
            {
                e.Cancel = true;
                ErrorTxtVin.SetError(TxtVin, "Моля, въведете VIN номер на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtVin.SetError(TxtVin, "");
            }
        }

        private void TxtRegNum_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtRegNum.Text))
            {
                e.Cancel = true;
                ErrorTxtRegNum.SetError(TxtRegNum, "Моля, въведете Регистрационен номер на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtRegNum.SetError(TxtRegNum, "");
            }
        }

        private void TxtOwner_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtOwner.Text))
            {
                e.Cancel = true;
                ErrorTxtOwner.SetError(TxtOwner, "Моля, въведете Имената на собственика на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtOwner.SetError(TxtOwner, "");
            }
        }

        private void TxtEgn_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEgn.Text))
            {
                e.Cancel = true;
                ErrorTxtEgn.SetError(TxtEgn, "Моля, въведете ЕГН-то на собственика на автомобила!");
                throw new ArgumentException();
            }
            else
            {
                e.Cancel = false;
                ErrorTxtEgn.SetError(TxtEgn, "");
            }
        }

        private void BtnClrForm_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void DtManfDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NudCubic_ValueChanged(object sender, EventArgs e)
        {

        }

        private void LblCubic_Click(object sender, EventArgs e)
        {

        }

        private void LblVin_Click(object sender, EventArgs e)
        {

        }

        private void LblBodyType_Click(object sender, EventArgs e)
        {

        }

        private void LblEgn_Click(object sender, EventArgs e)
        {

        }

        private void LblExtras_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
