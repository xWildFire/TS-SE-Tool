﻿/*
   Copyright 2016-2020 LIPtoH <liptoh.codebase@gmail.com>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace TS_SE_Tool
{
    public partial class FormMain
    {
        //User Trailer tab
        private void CreateTrailerPanelControls()
        {
            CreateTrailerPanelMainButtons();
            CreateTrailerPanelPartsControls();
        }

        private void CreateTrailerPanelMainButtons()
        {
            int pHeight = RepairImg.Height, pOffset = 5, tOffset = comboBoxUserTrailerCompanyTrailers.Location.Y;
            int topbutoffset = comboBoxUserTrailerCompanyTrailers.Location.X + comboBoxUserTrailerCompanyTrailers.Width + pOffset;

            Button buttonR = new Button();
            tableLayoutPanelUserTrailerControls.Controls.Add(buttonR, 1, 0);
            buttonR.FlatStyle = FlatStyle.Flat;
            buttonR.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonR.Name = "buttonTrailerRepair";
            buttonR.BackgroundImage = RepairImg;
            buttonR.BackgroundImageLayout = ImageLayout.Zoom;
            buttonR.Text = "";
            buttonR.FlatAppearance.BorderSize = 0;
            buttonR.Dock = DockStyle.Fill;
            buttonR.Click += new EventHandler(buttonTrailerRepair_Click);
            buttonR.EnabledChanged += new EventHandler(buttonTrailerElRepair_EnabledChanged);

            Button buttonInfo = new Button();
            tableLayoutPanelUserTrailerControls.Controls.Add(buttonInfo, 3, 0);
            buttonInfo.FlatStyle = FlatStyle.Flat;
            buttonInfo.Size = new Size(CustomizeImg.Width, CustomizeImg.Height);
            buttonInfo.Name = "buttonTrailerInfo";
            buttonInfo.BackgroundImage = ConvertBitmapToGrayscale(CustomizeImg);
            buttonInfo.BackgroundImageLayout = ImageLayout.Zoom;
            buttonInfo.Text = "";
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Enabled = false;
            buttonInfo.Dock = DockStyle.Fill;
        }

        private void CreateTrailerPanelPartsControls()
        {
            int pSkillsNameHeight = 32, pSkillsNameWidth = 32;

            string[] toolskillimgtooltip = new string[] { "Cargo", "Body", "Chassis", "Wheels" };
            Label partLabel, partnameLabel;
            Panel pbPanel;

            for (int i = 0; i < 4; i++)
            {
                //Create table layout
                TableLayoutPanel tbllPanel = new TableLayoutPanel();
                tableLayoutPanelTrailerDetails.Controls.Add(tbllPanel, 0, i);
                tbllPanel.Dock = DockStyle.Fill;
                tbllPanel.Margin = new Padding(0);
                //
                tbllPanel.Name = "tableLayoutPanelTrailerDetails" + toolskillimgtooltip[i];

                tbllPanel.ColumnCount = 3;
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                tbllPanel.RowCount = 2;
                tbllPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
                tbllPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                //

                FlowLayoutPanel flowPanel = new FlowLayoutPanel();
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Margin = new Padding(0);
                tbllPanel.SetColumnSpan(flowPanel, 2);
                tbllPanel.Controls.Add(flowPanel, 0, 0);
                //
                //Part type
                partLabel = new Label();
                partLabel.Name = "labelTrailerPartName" + toolskillimgtooltip[i];
                partLabel.Text = toolskillimgtooltip[i];
                partLabel.AutoSize = true;
                partLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                partLabel.MinimumSize = new Size(36, partLabel.Height);

                flowPanel.Controls.Add(partLabel);

                //Part name
                partnameLabel = new Label();
                partnameLabel.Name = "labelTrailerPartDataName" + i;
                partnameLabel.Text = "";
                partnameLabel.AutoSize = true;
                partnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

                flowPanel.Controls.Add(partnameLabel);

                //Part type image
                Panel imgpanel = new Panel();
                imgpanel.BorderStyle = BorderStyle.None;
                imgpanel.Size = new Size(pSkillsNameWidth, pSkillsNameHeight);
                imgpanel.Margin = new Padding(1);
                imgpanel.Name = "TrailerPartImg" + i.ToString();

                Bitmap bgimg = new Bitmap(TrailerPartsImg[i], pSkillsNameHeight, pSkillsNameWidth);
                imgpanel.BackgroundImage = bgimg;
                tbllPanel.Controls.Add(imgpanel, 0, 1);

                //Progress bar panel 
                pbPanel = new Panel();
                pbPanel.BorderStyle = BorderStyle.FixedSingle;
                pbPanel.Name = "progressbarTrailerPart" + i.ToString();
                pbPanel.Dock = DockStyle.Fill;
                tbllPanel.Controls.Add(pbPanel, 1, 1);

                //Repair button
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.Dock = DockStyle.Fill;
                button.Margin = new Padding(1);

                button.Name = "buttonTrailerElRepair" + i.ToString();
                button.BackgroundImage = RepairImg;
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.Text = "";
                button.FlatAppearance.BorderSize = 0;
                button.Click += new EventHandler(buttonTrailerElRepair_Click);
                button.EnabledChanged += new EventHandler(buttonTrailerElRepair_EnabledChanged);

                tbllPanel.Controls.Add(button, 2, 1);
            }
            //License plate
            Label labelPlate = new Label();
            labelPlate.Name = "labelUserTrailerLicensePlate";
            labelPlate.Text = "License plate";
            labelPlate.Margin = new Padding(0);
            labelPlate.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            labelPlate.TextAlign = ContentAlignment.MiddleCenter;

            Label lcPlate = new Label();
            lcPlate.Name = "labelLicensePlateTrailer";
            lcPlate.Text = "A 000 AA";
            lcPlate.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            lcPlate.Dock = DockStyle.Fill;
            lcPlate.TextAlign = ContentAlignment.MiddleLeft;

            tableLayoutPanelTrailerLP.Controls.Add(labelPlate, 0, 0);
            tableLayoutPanelTrailerLP.Controls.Add(lcPlate, 1, 0);

            //
            Panel LPpanel = new Panel();
            LPpanel.Dock = DockStyle.Fill;
            LPpanel.Margin = new Padding(0);
            LPpanel.Name = "TrailerLicensePlateIMG";
            LPpanel.BackgroundImageLayout = ImageLayout.Center;

            tableLayoutPanelTrailerLP.Controls.Add(LPpanel, 2, 0);
        }

        private void FillUserCompanyTrailerList()
        {
            DataTable combDT = new DataTable();
            DataColumn dc = new DataColumn("UserTrailerNameless", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("TrailerType", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("TrailerName", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("GarageName", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("DriverName", typeof(string));
            combDT.Columns.Add(dc);

            DataColumn dcDisplay = new DataColumn("DisplayMember");
            dcDisplay.Expression = string.Format("IIF(UserTrailerNameless <> 'null', '[' + {0} +'] ' + IIF(GarageName <> '', {1} +' || ','') + {2} + IIF(DriverName <> 'null', ' || In use - ' + {3},'')," +
                                                        "'-- NONE --')",
                                                "TrailerType", "GarageName", "TrailerName", "DriverName");
            combDT.Columns.Add(dcDisplay);
            //

            combDT.Rows.Add("null");

            foreach (KeyValuePair<string, UserCompanyTruckData> UserTrailer in UserTrailerDictionary)
            {
                if (UserTrailer.Value.Main)
                {
                    string trailername = "";
                    //
                    string tmpTrailerType = "", tmpTrailerkName = "", tmpGarageName = "", tmpDriverName = "";

                    if (UserTrailerDictionary[UserTrailer.Key].Users)
                    {
                        tmpTrailerType = "U";

                        tmpGarageName = GaragesList.Find(x => x.Trailers.Contains(UserTrailer.Key)).GarageNameTranslated;
                    }
                    else
                        tmpTrailerType = "Q";
                    //
                    string trailerdef = UserTrailerDictionary[UserTrailer.Key].Parts.Find(x => x.PartType == "trailerdef").PartNameless;

                    if (UserTrailerDefDictionary.Count > 0)
                    {
                        if (UserTrailerDefDictionary.ContainsKey(trailerdef))
                        {
                            string[] trailerDefPropertys = { "body_type", "axles", "chain_type" };
                            string[] trailerDefExtra = { "{0}", "{0} axles", "{0}" };

                            int iCounter = 0;
                            List<string> CurTrailerDef = UserTrailerDefDictionary[trailerdef];

                            bool wasfound = false;

                            foreach (string Property in trailerDefPropertys)
                            {
                                try
                                {
                                    string tmp = CurTrailerDef.Find(x => x.StartsWith(" " + Property + ":")).Split(':')[1].Trim(new char[] { ' ' }).Replace('_', ' ');

                                    if (wasfound)
                                        trailername += " | ";
                                    trailername += String.Format(trailerDefExtra[iCounter], System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tmp));

                                    wasfound = true;
                                }
                                catch { wasfound = false; }

                                iCounter++;
                            }

                            tmpTrailerkName = trailername;
                        }
                        else
                        {
                            tmpTrailerkName = trailerdef;
                        }
                    }
                    else
                    {
                        tmpTrailerkName = trailerdef;
                    }

                    tmpDriverName = UserDriverDictionary.Where(tX => tX.Value.AssignedTrailer == UserTrailer.Key)?.SingleOrDefault().Key ?? "null";

                    if (tmpDriverName != "null")
                        if (PlayerDataData.UserDriver == tmpDriverName)
                        {
                            tmpDriverName = "> " + Utilities.TextUtilities.FromHexToString(Globals.SelectedProfile);
                        }
                        else
                        {
                            DriverNames.TryGetValue(tmpDriverName, out string _resultvalue);

                            if (_resultvalue != null && _resultvalue != "")
                            {
                                tmpDriverName = _resultvalue.TrimStart(new char[] { '+' });
                            }
                        }

                    combDT.Rows.Add(UserTrailer.Key, tmpTrailerType, tmpTrailerkName, tmpGarageName, tmpDriverName); //(UserTrailer.Key, trailername);
                }
            }

            if (combDT.Rows.Count > 1)
            {
                comboBoxUserTrailerCompanyTrailers.Enabled = true;
            }
            else
            {
                comboBoxUserTrailerCompanyTrailers.Enabled = false;
            }

            comboBoxUserTrailerCompanyTrailers.ValueMember = "UserTrailerNameless";
            comboBoxUserTrailerCompanyTrailers.DisplayMember = "DisplayMember";
            comboBoxUserTrailerCompanyTrailers.DataSource = combDT;


            comboBoxUserTrailerCompanyTrailers.SelectedValue = PlayerDataData.UserCompanyAssignedTrailer;
        }
        //
        private void UpdateTrailerPanelDetails()
        {
            for (byte i = 0; i < 4; i++)
                UpdateTrailerPanelProgressBar(i);

            CheckTrailerRepair();

            //UpdateTrailerPanelCargo();
            UpdateTrailerPanelLicensePlate();
        }

        private void UpdateTrailerPanelProgressBar(byte _number)
        {
            UserTrailerDictionary.TryGetValue(comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTrailer);

            if (SelectedUserCompanyTrailer == null)
                return;

            string pnlname = "progressbarTrailerPart" + _number.ToString(), labelPartName = "labelTrailerPartDataName" + _number.ToString();

            //Progres bar
            Panel pbPanel = groupBoxUserTrailerTrailerDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

            //Part name
            Label pnLabel = groupBoxUserTrailerTrailerDetails.Controls.Find(labelPartName, true).FirstOrDefault() as Label;

            //Repair button
            Button repairButton = groupBoxUserTrailerTrailerDetails.Controls.Find("buttonTrailerElRepair" + _number, true).FirstOrDefault() as Button;

            if (pbPanel != null)
            {
                List<UserCompanyTruckDataPart> DataPart = null;

                try
                {
                    switch (_number)
                    {
                        case 0:
                            {
                                DataPart = SelectedUserCompanyTrailer.Parts.FindAll(xp => xp.PartType == "trailerdata");
                                break;
                            }
                        case 1:
                            DataPart = SelectedUserCompanyTrailer.Parts.FindAll(xp => xp.PartType == "body");
                            break;
                        case 2:
                            DataPart = SelectedUserCompanyTrailer.Parts.FindAll(xp => xp.PartType == "chassis");
                            break;
                        case 3:
                            DataPart = SelectedUserCompanyTrailer.Parts.FindAll(xp => xp.PartType == "tire");
                            break;
                    }
                }
                catch
                {
                    repairButton.Enabled = false;
                    return;
                }

                decimal _wear = 0;
                byte partCount = 0;

                if (DataPart != null && DataPart.Count > 0)
                {
                    if (pnLabel != null)
                        if (_number != 0)
                        {
                            pnLabel.Text = DataPart[0].PartData.Find(xl => xl.StartsWith(" data_path:")).Split(new char[] { '"' })[1].Split(new char[] { '/' }).Last().Split(new char[] { '.' })[0];
                        }
                        else
                        {
                            var tmp = UserDriverDictionary.Select(tx => tx.Value)
                                    .Where(tX => tX.AssignedTrailer == comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString()).ToList();

                            if (tmp != null && tmp.Count > 0)
                            {
                                string tmpCargo = tmp[0].DriverJob.Cargo;

                                if (CargoLngDict.TryGetValue(tmpCargo, out string value))
                                {
                                    if (value != null && value != "")
                                    {
                                        pnLabel.Text = value;
                                    }
                                    else
                                    {
                                        string CapName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);

                                        pnLabel.Text = CapName;
                                    }
                                }
                            }
                            else
                            {
                                repairButton.Enabled = false;
                                pbPanel.BackgroundImage = null;
                                pnLabel.Text = "";
                                return;
                            }
                        }

                    foreach (UserCompanyTruckDataPart tmpPartData in DataPart)
                    {
                        try
                        {
                            string tmpWear = tmpPartData.PartData.Find(xl => xl.StartsWith(" wear:") || xl.StartsWith(" cargo_damage:")).Split(new char[] { ' ' })[2];
                            decimal _tmpWear = 0;

                            if (tmpWear != "0" && tmpWear != "1")
                            {
                                _tmpWear = Utilities.NumericUtilities.HexFloatToDecimalFloat(tmpWear);
                            }
                            else if (tmpWear == "1")
                            {
                                _tmpWear = 1;
                            }

                            _wear += _tmpWear;
                            partCount++;
                        }
                        catch
                        { }
                    }
                }
                else
                {
                    pnLabel.Text = "none";
                    repairButton.Enabled = false;
                    return;
                }

                _wear = _wear / partCount;

                if (_wear == 0)
                    repairButton.Enabled = false;
                else
                    repairButton.Enabled = true;

                //
                SolidBrush ppen = new SolidBrush(GetProgressbarColor(_wear));

                int x = 0, y = 0, pnlwidth = (int)(pbPanel.Width * (1 - _wear));

                Bitmap progress = new Bitmap(pbPanel.Width, pbPanel.Height);

                Graphics g = Graphics.FromImage(progress);
                g.FillRectangle(ppen, x, y, pnlwidth, pbPanel.Height);

                int fontSize = 12;
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    ((int)((1 - _wear) * 100)).ToString() + " %",   // text to draw
                    FontFamily.GenericSansSerif,                    // or any other font family
                    (int)FontStyle.Bold,                            // font style (bold, italic, etc.)
                    g.DpiY * fontSize / 72,                         // em size
                    new Rectangle(0, 0, pbPanel.Width, pbPanel.Height),     // location where to draw text
                    sf);                                            // set options here (e.g. center alignment)
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(Brushes.Black, p);
                g.DrawPath(Pens.Black, p);

                pbPanel.BackgroundImage = progress;
            }
        }

        private void CheckTrailerRepair()
        {
            bool repairEnabled = false;
            Button repairTrailer = tableLayoutPanelUserTrailerControls.Controls.Find("buttonTrailerRepair", true).FirstOrDefault() as Button;

            for (byte i = 0; i < 4; i++)
            {
                try
                {
                    Button tmp = groupBoxUserTrailerTrailerDetails.Controls.Find("buttonTrailerElRepair" + i, true).FirstOrDefault() as Button;
                    if (tmp.Enabled)
                    {
                        repairEnabled = true;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }

            repairTrailer.Enabled = repairEnabled;
        }

        private void UpdateTrailerPanelCargo()
        {

        }

        private void UpdateTrailerPanelLicensePlate()
        {
            UserTrailerDictionary.TryGetValue(comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTrailer);
            
            string LicensePlate = SelectedUserCompanyTrailer.Parts.Find(xp => xp.PartType == "trailerdata").PartData.Find(xl => xl.StartsWith(" license_plate:")).Split(new char[] { '"' })[1];

            SCS.SCSLicensePlate thisLP = new SCS.SCSLicensePlate(LicensePlate, SCS.SCSLicensePlate.LPtype.Truck);

            //Find label control
            Label lpText = groupBoxUserTrailerTrailerDetails.Controls.Find("labelLicensePlateTrailer", true).FirstOrDefault() as Label;
            if (lpText != null)
            {
                lpText.Text = thisLP.LicensePlateTXT + " | ";

                string value = null;
                CountriesLngDict.TryGetValue(thisLP.SourceLPCountry, out value);

                if (value != null && value != "")
                {
                    lpText.Text += value;
                }
                else
                {
                    string CapName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(thisLP.SourceLPCountry);
                    lpText.Text += CapName;
                }
            }

            //
            Panel lpPanel = groupBoxUserTrailerTrailerDetails.Controls.Find("TrailerLicensePlateIMG", true).FirstOrDefault() as Panel;
            if (lpPanel != null)
            {
                lpPanel.BackgroundImage = Utilities.TS_Graphics.ResizeImage(thisLP.LicensePlateIMG, LicensePlateWidth[GameType], 32); //ETS - 128x32 or ATS - 128x64 | 64x32
            }
        }

        //Events
        private void comboBoxCompanyTrailers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbbx = sender as ComboBox;

            if (cmbbx.SelectedIndex != -1 && cmbbx.SelectedValue.ToString() != "null")
            {
                ToggleTrailerPartsCondition(true);

                buttonUserTrailerSelectCurrent.Enabled = true;
                tableLayoutPanelUserTrailerControls.Enabled = true;

                groupBoxUserTrailerTrailerDetails.Enabled = true;
                groupBoxUserTrailerShareTrailerSettings.Enabled = true;

                UpdateTrailerPanelDetails();
            }
            else
            {
                ToggleTrailerPartsCondition(false);

                if (!comboBoxUserTrailerCompanyTrailers.Enabled)
                    buttonUserTrailerSelectCurrent.Enabled = false;

                tableLayoutPanelUserTrailerControls.Enabled = false;

                groupBoxUserTrailerTrailerDetails.Enabled = false;
                groupBoxUserTrailerShareTrailerSettings.Enabled = false;
            }
        }
        
        private void groupBoxUserTrailerTrailerDetails_EnabledChanged(object sender, EventArgs e)
        {
            ToggleVisualTrailerDetails(groupBoxUserTrailerTrailerDetails.Enabled);
        }

        private void tableLayoutPanelUserTrailerControls_EnabledChanged(object sender, EventArgs e)
        {
            ToggleVisualTrailerControls(tableLayoutPanelUserTrailerControls.Enabled);
        }

        private void ToggleVisualTrailerDetails(bool _state)
        {
            for (int i = 0; i < 4; i++)
            {
                Control[] tmp = tabControlMain.TabPages["tabPageTrailer"].Controls.Find("buttonTrailerElRepair" + i.ToString(), true);
                if (_state && tmp[0].Enabled)
                    tmp[0].BackgroundImage = RepairImg;
                else
                    tmp[0].BackgroundImage = ConvertBitmapToGrayscale(RepairImg);
            }
        }

        private void ToggleVisualTrailerControls(bool _state)
        {
            Control TMP;

            string[] buttons = { "buttonTrailerRepair", "buttonTrailerInfo" };
            Image[] images = { RepairImg, CustomizeImg };

            for (int i = 0; i < buttons.Count(); i++)
            {
                try
                {
                    TMP = tabControlMain.TabPages["tabPageTrailer"].Controls.Find(buttons[i], true)[0];
                }
                catch
                {
                    break;
                }

                if (_state && TMP.Enabled)
                    TMP.BackgroundImage = images[i];
                else
                    TMP.BackgroundImage = ConvertBitmapToGrayscale(images[i]);
            }
        }
        
        private void ToggleTrailerPartsCondition(bool _state)
        {
            if (!_state)
            {
                string lblname, pnlname;

                for (int i = 0; i < 4; i++)
                {
                    lblname = "labelTrailerPartDataName" + i.ToString();
                    Label pnLabel = groupBoxUserTrailerTrailerDetails.Controls.Find(lblname, true).FirstOrDefault() as Label;

                    if (pnLabel != null)
                        pnLabel.Text = "";

                    pnlname = "progressbarTrailerPart" + i.ToString();
                    Panel pbPanel = groupBoxUserTrailerTrailerDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

                    if (pbPanel != null)
                        pbPanel.BackgroundImage = null;

                }

                string lblLCname = "labelLicensePlateTrailer";
                Label lblLC = groupBoxUserTrailerTrailerDetails.Controls.Find(lblLCname, true).FirstOrDefault() as Label;

                if (lblLC != null)
                    lblLC.Text = "A 000 AA";

                pnlname = "TrailerLicensePlateIMG";
                Panel LPPanel = groupBoxUserTrailerTrailerDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

                if (LPPanel != null)
                    LPPanel.BackgroundImage = null;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    Button repairButton = groupBoxUserTrailerTrailerDetails.Controls.Find("buttonTrailerElRepair" + i, true).FirstOrDefault() as Button;

                    if (repairButton != null && repairButton.Enabled)
                        repairButton.BackgroundImage = RepairImg;
                }
            }
        }
        //Buttons
        //Main
        public void buttonTrailerRepair_Click(object sender, EventArgs e)
        {
            string[] PartList = { "trailerdata", "body", "chassis", "tire" };
            string trailerNameless = "";

            trailerNameless = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();

            StartTrailerParts:

            foreach (string tempPart in PartList)
            {
                foreach (UserCompanyTruckDataPart temp in UserTrailerDictionary[trailerNameless].Parts.FindAll(x => x.PartType == tempPart))
                {
                    string partNameless = temp.PartNameless;

                    int i = 0;

                    foreach (string temp2 in temp.PartData)
                    {
                        if (temp2.StartsWith(" wear:"))
                        {
                            UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                            break;
                        }
                        else
                        if (temp2.StartsWith(" cargo_damage:"))
                        {
                            UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "trailerdata").PartData[i] = " cargo_damage: 0";
                            break;
                        }
                        i++;
                    }
                }
            }

            UserCompanyTruckDataPart slavetrailer = UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "slavetrailer");

            if (slavetrailer != null)
            {
                trailerNameless = slavetrailer.PartNameless;
                goto StartTrailerParts;
            }

            for (byte i = 0; i < 5; i++)
                UpdateTrailerPanelProgressBar(i);

            CheckTrailerRepair();
        }
        //
        private void buttonUserTrailerSelectCurrent_Click(object sender, EventArgs e)
        {
            comboBoxUserTrailerCompanyTrailers.SelectedValue = PlayerDataData.UserCompanyAssignedTrailer;
        }

        private void buttonUserTrailerSwitchCurrent_Click(object sender, EventArgs e)
        {
            PlayerDataData.UserCompanyAssignedTrailer = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();
        }
        //Details
        public void buttonTrailerElRepair_Click(object sender, EventArgs e)
        {
            Button curbtn = sender as Button;
            byte bi = Convert.ToByte(curbtn.Name.Substring(21));

            string[] PartList = { "trailerdata", "body", "chassis", "tire" };
            string trailerNameless = "";

            trailerNameless = comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString();

            StartTrailerParts:

            foreach (UserCompanyTruckDataPart temp in UserTrailerDictionary[trailerNameless].Parts.FindAll(x => x.PartType == PartList[bi]))
            {
                string partNameless = temp.PartNameless;

                int i = 0;

                foreach (string temp2 in temp.PartData)
                {
                    if (temp2.StartsWith(" wear:"))
                    {
                        UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                        break;
                    }
                    else
                    if (temp2.StartsWith(" cargo_damage:"))
                    {
                        UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "trailerdata").PartData[i] = " cargo_damage: 0";
                        break;
                    }
                    i++;
                }
            }

            UserCompanyTruckDataPart slavetrailer = UserTrailerDictionary[trailerNameless].Parts.Find(x => x.PartType == "slavetrailer");

            if (slavetrailer != null)
            {
                trailerNameless = slavetrailer.PartNameless;
                goto StartTrailerParts;
            }

            UpdateTrailerPanelProgressBar(bi);

            CheckTrailerRepair();
        }

        public void buttonTrailerElRepair_EnabledChanged(object sender, EventArgs e)
        {
            Button tmp = sender as Button;

            if (tmp.Enabled)
                tmp.BackgroundImage = RepairImg;
            else
                tmp.BackgroundImage = ConvertBitmapToGrayscale(RepairImg);
        }

        private void buttonTrailersCapricornsPaint_Click(object sender, EventArgs e)
        {
            try
            {

                string trailerdef = UserTrailerDictionary[comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString()].Parts.Find(x => x.PartType == "trailerdef").PartNameless;

                string trailFull = "";
                try
                {
                    string source_name = UserTrailerDefDictionary[trailerdef].Find(x => x.StartsWith(" source_name:")).Split(':')[1];

                    if (!source_name.Contains("null"))
                    {
                        trailFull += source_name.Split(new char[] { '"' })[1].Trim(new char[] { ' ' });
                    }
                }
                catch { }

                string decode = "1F8B08006792995E00FF9590CD8AC2301485F782EF105C88036262D39FD4A77031FB70937B3366ACA63445C5A79FA432C5590E04F241BE732EB99F03F88E8623F8EBB85CB00BC4B31EB40D5D180E6CB396AE295461CA2D4B28B0125064C4B67158AA8FDFC4D79C105B96CFFC62DEBA505949A5CA054649DADB3A63BA5B059413AE8333FD196E1D004EC39BCA89DA4D5897240DBE02BEFF8F6F20BEF743EB8AE973227B56C8469A2A7B77826488440823E81EC6D381AD3892E3373A79DB111F5F8BD3E17E25E4D1C69D090FDEE73DEAEF60F8D35F60AF93B58BDEAF968B1FEF4F44A869010000";
                if (trailFull.Equals("trailer_def.krone.coolliner.single_3.reefer"))
                {
                    decode = "1F8B080010C0995E00FF6D50418AC3300CBC17FA07D3436921D44E9C364E5FB187BD1BD9965B6FDC3838610BFBFA8D1A36F4B030A041D28CC47C660811F307847EDA6ED803C64E676D534CF9CA0E7BE99B4A55A62ED84C853B0BA888BAB6F1AE56C73FC56D55888211D68979F372CA4AAC15191825B1B417A2736D1520297C840E57415930C23208C37F7D0323BE1D80D657AF6705195B211B69CEB4F7449837C4CC1C4CA00798EE57B6E30E3DFFC67BB011F9B404A1D3B347C7BB9C7A3CD994620C3D663E503EFA2B19FE131E509EC61076DBCD2F03D95BF33D010000";
                }
                else if (trailFull.Equals("trailer_def.krone.profiliner.single_3.curtain"))
                {
                    decode = "1F8B080044C0995E00FF4D90C16AC3300C86EF85BE83E9616C506A274E17A74FB1C3EE46B6E5558B1B1B27ACD0A76FBC765940A01F49FF27A1CF0C14307F000DD376C32E30F63A6B1B43CC27F6FA227D5BABDA347B364BE18E02EA225DD77AD7A8B73FC7D7E2107B5662E99815CB292BB151056094C4CABE1739E74E0116870FD0E31A553D513E505AEAD5FF0A0323AE1640E7EBDF6345015B215B698E65EE8A304F88593998402798CE27B6E30E3DFFC133D9807C7A3C42C7EB808EF7390E7848397A0A3460E6A93C487F47C36F74814AA7C348B4DB6EEE3F65122B40010000";
                }
                else if (trailFull.Equals("trailer_def.krone.dryliner.single_3.dryvan"))
                {
                    decode = "1F8B080071C0995E00FF6D50C18AC23010BD0BFE43F0B028149336759BFA157BD87B982413CD3636252D2BBB5F6F47B178101ECC6366DE9BE17D670811F317847E5AAFD805C64E676D534CF9C8B61FD23795AA4C5DB0990A77105011756DE35DAD764FC56951888211968979F172CA4AAC15191825B1B49F44E7DA2A4052F8081D2E82B26084C7200CEFFA06467C3900ADAFEECF0A32B64236D21C68EF8A306F88993998400F309D8F6CC31D7AFE8BE76023F2E911844ED71E1DEF72EA71EFF25F0C3D663E503CFA2719FE1F2E506AB71F43D8AC5737E481114E3E010000";
                }


                string inputData = Utilities.ZipDataUtilitiescs.unzipText(decode);
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TrailerPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTrailerDictionary[comboBoxUserTrailerCompanyTrailers.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

                    MessageBox.Show("Paint data  has been inserted.");
                }
                else
                    MessageBox.Show("Wrong data. Expected Paint data but\r\n" + Lines[0] + "\r\nwas found.");
            }
            catch
            {
                MessageBox.Show("Something gone wrong with decoding.");
            }
        }
        //end User Trailer tab
    }
}