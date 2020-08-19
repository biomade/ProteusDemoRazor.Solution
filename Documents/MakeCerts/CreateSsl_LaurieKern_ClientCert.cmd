makecert.exe ^
-n "CN=KERN.LAURIE.JANE.1264693957,OU=CONTRACTOR,OU=PKI,OU=DoD,O=U.S. Government,C=US" ^
-iv CARoot.pvk ^
-ic CARoot.cer ^
-pe ^
-a sha512 ^
-len 4096 ^
-b 08/01/2020 ^
-e 08/01/2023 ^
-sky exchange ^
-eku 1.3.6.1.5.5.7.3.2 ^
-sv %1.pvk ^
%1.cer ^
-sr CurrentUser ^
-ss My 


pvk2pfx.exe ^
-pvk %1.pvk ^
-spc %1.cer ^
-pfx %1.pfx ^
-po Test123