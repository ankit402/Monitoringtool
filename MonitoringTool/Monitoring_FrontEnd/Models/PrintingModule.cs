using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoring_FrontEnd.Models
{
    public class PrintingModule
    {[Key]
        public int print_id { get; set; }
        public string PrintHead { get; set; }
        public string PrinterSerialNumber { get; set; }
        public string PrinterModel { get; set; }
        public string PrinterMessageNumber { get; set; }
        public string PrinterStatus { get; set; }
        public string PrinterVersion { get; set; }
        public string PrintEngineType { get; set; }
        public string PrinterAddress { get; set; }
        public string ConnectionPortType { get; set; }
        public string ColorPrintResolution { get; set; }
        public string ConnectionProtocol { get; set; }
        public string EmbosserVersion { get; set; }
        public string Laminator { get; set; }
        public string LaminatorFirmwareVersion { get; set; }
        public string LaminatorImpresser { get; set; }
        public string LaminatorScanner { get; set; }
        public string LaserFirmwareVersion { get; set; }
        public string LockState { get; set; }
        public string ModuleEmbosser { get; set; }
        public string MonochromePrintResolution { get; set; }
        public string MultiHopperVersion { get; set; }
        public string TopcoatPrintResolution { get; set; }
        public string OptionDuplex { get; set; }
        public string OptionInputhopper { get; set; }
        public string OptionLaser { get; set; }
        public string OptionLaserVisionRegistration { get; set; }
        public string OptionObscureBlackPanel { get; set; }
        public string OptionLocks { get; set; }
        public string OptionMagstripe { get; set; }
        public string OptionSecondaryMagstripeJIS { get; set; }
        public string OptionPrinterBarcodeReader { get; set; }
        public string OptionRewrite { get; set; }
        public string OptionSmartcard { get; set; }
        public string OptionTactileImpresser { get; set; }
        public string PrinterColorMode { get; set; }
        public string CardsPickedSinceCleaningCard { get; set; }
        public string CleaningCardsRun { get; set; }
        public string CurrentCompleted { get; set; }
        public string CurrentLost { get; set; }
        public string CurrentPicked { get; set; }
        public string CurrentPickedExceptionSlot { get; set; }
        public string CurrentPickedInputHopper1 { get; set; }
        public string CurrentPickedInputHopper2 { get; set; }
        public string CurrentPickedInputHopper3 { get; set; }
        public string CurrentPickedInputHopper4 { get; set; }
        public string CurrentPickedInputHopper5 { get; set; }
        public string CurrentPickedInputHopper6 { get; set; }
        public string CurrentRejected { get; set; }
        public string TotalCompleted { get; set; }
        public string TotalLost { get; set; }
        public string TotalPicked { get; set; }
        public string TotalPickedExceptionSlot { get; set; }
        public string TotalPickedInputHopper1 { get; set; }
        public string TotalPickedInputHopper2 { get; set; }
        public string TotalPickedInputHopper3 { get; set; }
        public string TotalPickedInputHopper4 { get; set; }
        public string TotalPickedInputHopper5 { get; set; }
        public string TotalPickedInputHopper6 { get; set; }
        public string TotalRejected { get; set; }
        public string IndentRibbon { get; set; }
        public string IndentRibbonLotCode { get; set; }
        public string IndentRibbonPartNumber { get; set; }
        public string IndentRibbonRemaining { get; set; }
        public string IndentRibbonSerialNumber { get; set; }
        public string L1LaminateLotCode { get; set; }
        public string L1LaminatePartNumber { get; set; }
        public string L1LaminateRemaining { get; set; }
        public string L1LaminateSerialNumber { get; set; }
        public string L1LaminateType { get; set; }
        public string L2LaminateLotCode { get; set; }
        public string L2LaminatePartNumber { get; set; }
        public string L2LaminateRemaining { get; set; }
        public string L2LaminateSerialNumber { get; set; }
        public string L2LaminateType { get; set; }
        public string PrintRibbonLotCode { get; set; }
        public string PrintRibbonPartNumber { get; set; }
        public string PrintRibbonSerialNumber { get; set; }
        public string PrintRibbonType { get; set; }
        public string PrintRibbonRegionCode { get; set; }
        public string RibbonRemaining { get; set; }
        public string RetransferFilmLotCode { get; set; }
        public string RetransferFilmPartNumber { get; set; }
        public string RetransferFilmRemaining { get; set; }
        public string RetransferFilmSerialNumber { get; set; }
        public string TactileImpresserLotCode { get; set; }
        public string TactileImpresserPartNumber { get; set; }
        public string TactileImpresserRemaining { get; set; }
        public string TactileImpresserSerialNumber { get; set; }
        public string TactileImpresserType { get; set; }
        public string TopperRibbonLotCode { get; set; }
        public string TopperRibbonPartNumber { get; set; }
        public string TopperRibbonRemaining { get; set; }
        public string TopperRibbonSerialNumber { get; set; }
        public string TopperRibbonType { get; set; }
      
    }
}
