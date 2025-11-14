#include"Board.h"

const std::array<uint16_t, 3516> indextoMove{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 22, 33, 44, 55, 66, 77, 88, 99, 10, 20, 30, 40, 50, 60, 70, 80, 90, 102, 103, 104, 105, 106, 107, 108, 109, 112, 123, 134, 145, 156, 167, 178, 189, 111, 121, 131, 141, 151, 161, 171, 181, 191, 110, 100, 203, 204, 205, 206, 207, 208, 209, 213, 224, 235, 246, 257, 268, 279, 212, 222, 232, 242, 252, 262, 272, 282, 292, 211, 220, 201, 200, 304, 305, 306, 307, 308, 309, 314, 325, 336, 347, 358, 369, 313, 323, 333, 343, 353, 363, 373, 383, 393, 312, 321, 330, 302, 301, 300, 405, 406, 407, 408, 409, 415, 426, 437, 448, 459, 414, 424, 434, 444, 454, 464, 474, 484, 494, 413, 422, 431, 440, 403, 402, 401, 400, 506, 507, 508, 509, 516, 527, 538, 549, 515, 525, 535, 545, 555, 565, 575, 585, 595, 514, 523, 532, 541, 550, 504, 503, 502, 501, 500, 607, 608, 609, 617, 628, 639, 616, 626, 636, 646, 656, 666, 676, 686, 696, 615, 624, 633, 642, 651, 660, 605, 604, 603, 602, 601, 600, 708, 709, 718, 729, 717, 727, 737, 747, 757, 767, 777, 787, 797, 716, 725, 734, 743, 752, 761, 770, 706, 705, 704, 703, 702, 701, 700, 809, 819, 818, 828, 838, 848, 858, 868, 878, 888, 898, 817, 826, 835, 844, 853, 862, 871, 880, 807, 806, 805, 804, 803, 802, 801, 800, 919, 929, 939, 949, 959, 969, 979, 989, 999, 918, 927, 936, 945, 954, 963, 972, 981, 990, 908, 907, 906, 905, 904, 903, 902, 901, 900, 1000, 1001, 1011, 1012, 1013, 1014, 1015, 1016, 1017, 1018, 1019, 1021, 1032, 1043, 1054, 1065, 1076, 1087, 1098, 1020, 1030, 1040, 1050, 1060, 1070, 1080, 1090, 1101, 1102, 1112, 1113, 1114, 1115, 1116, 1117, 1118, 1119, 1122, 1133, 1144, 1155, 1166, 1177, 1188, 1199, 1121, 1131, 1141, 1151, 1161, 1171, 1181, 1191, 1120, 1110, 1100, 1202, 1203, 1213, 1214, 1215, 1216, 1217, 1218, 1219, 1223, 1234, 1245, 1256, 1267, 1278, 1289, 1222, 1232, 1242, 1252, 1262, 1272, 1282, 1292, 1221, 1230, 1211, 1210, 1201, 1303, 1304, 1314, 1315, 1316, 1317, 1318, 1319, 1324, 1335, 1346, 1357, 1368, 1379, 1323, 1333, 1343, 1353, 1363, 1373, 1383, 1393, 1322, 1331, 1340, 1312, 1311, 1310, 1302, 1404, 1405, 1415, 1416, 1417, 1418, 1419, 1425, 1436, 1447, 1458, 1469, 1424, 1434, 1444, 1454, 1464, 1474, 1484, 1494, 1423, 1432, 1441, 1450, 1413, 1412, 1411, 1410, 1403, 1505, 1506, 1516, 1517, 1518, 1519, 1526, 1537, 1548, 1559, 1525, 1535, 1545, 1555, 1565, 1575, 1585, 1595, 1524, 1533, 1542, 1551, 1560, 1514, 1513, 1512, 1511, 1510, 1504, 1606, 1607, 1617, 1618, 1619, 1627, 1638, 1649, 1626, 1636, 1646, 1656, 1666, 1676, 1686, 1696, 1625, 1634, 1643, 1652, 1661, 1670, 1615, 1614, 1613, 1612, 1611, 1610, 1605, 1707, 1708, 1718, 1719, 1728, 1739, 1727, 1737, 1747, 1757, 1767, 1777, 1787, 1797, 1726, 1735, 1744, 1753, 1762, 1771, 1780, 1716, 1715, 1714, 1713, 1712, 1711, 1710, 1706, 1808, 1809, 1819, 1829, 1828, 1838, 1848, 1858, 1868, 1878, 1888, 1898, 1827, 1836, 1845, 1854, 1863, 1872, 1881, 1890, 1817, 1816, 1815, 1814, 1813, 1812, 1811, 1810, 1807, 1909, 1929, 1939, 1949, 1959, 1969, 1979, 1989, 1999, 1928, 1937, 1946, 1955, 1964, 1973, 1982, 1991, 1918, 1917, 1916, 1915, 1914, 1913, 1912, 1911, 1910, 1908, 2010, 2000, 2011, 2002, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028, 2029, 2031, 2042, 2053, 2064, 2075, 2086, 2097, 2030, 2040, 2050, 2060, 2070, 2080, 2090, 2111, 2101, 2112, 2103, 2122, 2123, 2124, 2125, 2126, 2127, 2128, 2129, 2132, 2143, 2154, 2165, 2176, 2187, 2198, 2131, 2141, 2151, 2161, 2171, 2181, 2191, 2130, 2120, 2110, 2212, 2202, 2213, 2204, 2223, 2224, 2225, 2226, 2227, 2228, 2229, 2233, 2244, 2255, 2266, 2277, 2288, 2299, 2232, 2242, 2252, 2262, 2272, 2282, 2292, 2231, 2240, 2221, 2220, 2211, 2200, 2313, 2303, 2314, 2305, 2324, 2325, 2326, 2327, 2328, 2329, 2334, 2345, 2356, 2367, 2378, 2389, 2333, 2343, 2353, 2363, 2373, 2383, 2393, 2332, 2341, 2350, 2322, 2321, 2320, 2312, 2301, 2414, 2404, 2415, 2406, 2425, 2426, 2427, 2428, 2429, 2435, 2446, 2457, 2468, 2479, 2434, 2444, 2454, 2464, 2474, 2484, 2494, 2433, 2442, 2451, 2460, 2423, 2422, 2421, 2420, 2413, 2402, 2515, 2505, 2516, 2507, 2526, 2527, 2528, 2529, 2536, 2547, 2558, 2569, 2535, 2545, 2555, 2565, 2575, 2585, 2595, 2534, 2543, 2552, 2561, 2570, 2524, 2523, 2522, 2521, 2520, 2514, 2503, 2616, 2606, 2617, 2608, 2627, 2628, 2629, 2637, 2648, 2659, 2636, 2646, 2656, 2666, 2676, 2686, 2696, 2635, 2644, 2653, 2662, 2671, 2680, 2625, 2624, 2623, 2622, 2621, 2620, 2615, 2604, 2717, 2707, 2718, 2709, 2728, 2729, 2738, 2749, 2737, 2747, 2757, 2767, 2777, 2787, 2797, 2736, 2745, 2754, 2763, 2772, 2781, 2790, 2726, 2725, 2724, 2723, 2722, 2721, 2720, 2716, 2705, 2818, 2808, 2819, 2829, 2839, 2838, 2848, 2858, 2868, 2878, 2888, 2898, 2837, 2846, 2855, 2864, 2873, 2882, 2891, 2827, 2826, 2825, 2824, 2823, 2822, 2821, 2820, 2817, 2806, 2919, 2909, 2939, 2949, 2959, 2969, 2979, 2989, 2999, 2938, 2947, 2956, 2965, 2974, 2983, 2992, 2928, 2927, 2926, 2925, 2924, 2923, 2922, 2921, 2920, 2918, 2907, 3020, 3010, 3000, 3021, 3012, 3003, 3031, 3032, 3033, 3034, 3035, 3036, 3037, 3038, 3039, 3041, 3052, 3063, 3074, 3085, 3096, 3040, 3050, 3060, 3070, 3080, 3090, 3121, 3111, 3101, 3122, 3113, 3104, 3132, 3133, 3134, 3135, 3136, 3137, 3138, 3139, 3142, 3153, 3164, 3175, 3186, 3197, 3141, 3151, 3161, 3171, 3181, 3191, 3140, 3130, 3120, 3222, 3212, 3202, 3223, 3214, 3205, 3233, 3234, 3235, 3236, 3237, 3238, 3239, 3243, 3254, 3265, 3276, 3287, 3298, 3242, 3252, 3262, 3272, 3282, 3292, 3241, 3250, 3231, 3230, 3221, 3210, 3323, 3313, 3303, 3324, 3315, 3306, 3334, 3335, 3336, 3337, 3338, 3339, 3344, 3355, 3366, 3377, 3388, 3399, 3343, 3353, 3363, 3373, 3383, 3393, 3342, 3351, 3360, 3332, 3331, 3330, 3322, 3311, 3300, 3424, 3414, 3404, 3425, 3416, 3407, 3435, 3436, 3437, 3438, 3439, 3445, 3456, 3467, 3478, 3489, 3444, 3454, 3464, 3474, 3484, 3494, 3443, 3452, 3461, 3470, 3433, 3432, 3431, 3430, 3423, 3412, 3401, 3525, 3515, 3505, 3526, 3517, 3508, 3536, 3537, 3538, 3539, 3546, 3557, 3568, 3579, 3545, 3555, 3565, 3575, 3585, 3595, 3544, 3553, 3562, 3571, 3580, 3534, 3533, 3532, 3531, 3530, 3524, 3513, 3502, 3626, 3616, 3606, 3627, 3618, 3609, 3637, 3638, 3639, 3647, 3658, 3669, 3646, 3656, 3666, 3676, 3686, 3696, 3645, 3654, 3663, 3672, 3681, 3690, 3635, 3634, 3633, 3632, 3631, 3630, 3625, 3614, 3603, 3727, 3717, 3707, 3728, 3719, 3738, 3739, 3748, 3759, 3747, 3757, 3767, 3777, 3787, 3797, 3746, 3755, 3764, 3773, 3782, 3791, 3736, 3735, 3734, 3733, 3732, 3731, 3730, 3726, 3715, 3704, 3828, 3818, 3808, 3829, 3839, 3849, 3848, 3858, 3868, 3878, 3888, 3898, 3847, 3856, 3865, 3874, 3883, 3892, 3837, 3836, 3835, 3834, 3833, 3832, 3831, 3830, 3827, 3816, 3805, 3929, 3919, 3909, 3949, 3959, 3969, 3979, 3989, 3999, 3948, 3957, 3966, 3975, 3984, 3993, 3938, 3937, 3936, 3935, 3934, 3933, 3932, 3931, 3930, 3928, 3917, 3906, 4030, 4020, 4010, 4000, 4031, 4022, 4013, 4004, 4041, 4042, 4043, 4044, 4045, 4046, 4047, 4048, 4049, 4051, 4062, 4073, 4084, 4095, 4050, 4060, 4070, 4080, 4090, 4131, 4121, 4111, 4101, 4132, 4123, 4114, 4105, 4142, 4143, 4144, 4145, 4146, 4147, 4148, 4149, 4152, 4163, 4174, 4185, 4196, 4151, 4161, 4171, 4181, 4191, 4150, 4140, 4130, 4232, 4222, 4212, 4202, 4233, 4224, 4215, 4206, 4243, 4244, 4245, 4246, 4247, 4248, 4249, 4253, 4264, 4275, 4286, 4297, 4252, 4262, 4272, 4282, 4292, 4251, 4260, 4241, 4240, 4231, 4220, 4333, 4323, 4313, 4303, 4334, 4325, 4316, 4307, 4344, 4345, 4346, 4347, 4348, 4349, 4354, 4365, 4376, 4387, 4398, 4353, 4363, 4373, 4383, 4393, 4352, 4361, 4370, 4342, 4341, 4340, 4332, 4321, 4310, 4434, 4424, 4414, 4404, 4435, 4426, 4417, 4408, 4445, 4446, 4447, 4448, 4449, 4455, 4466, 4477, 4488, 4499, 4454, 4464, 4474, 4484, 4494, 4453, 4462, 4471, 4480, 4443, 4442, 4441, 4440, 4433, 4422, 4411, 4400, 4535, 4525, 4515, 4505, 4536, 4527, 4518, 4509, 4546, 4547, 4548, 4549, 4556, 4567, 4578, 4589, 4555, 4565, 4575, 4585, 4595, 4554, 4563, 4572, 4581, 4590, 4544, 4543, 4542, 4541, 4540, 4534, 4523, 4512, 4501, 4636, 4626, 4616, 4606, 4637, 4628, 4619, 4647, 4648, 4649, 4657, 4668, 4679, 4656, 4666, 4676, 4686, 4696, 4655, 4664, 4673, 4682, 4691, 4645, 4644, 4643, 4642, 4641, 4640, 4635, 4624, 4613, 4602, 4737, 4727, 4717, 4707, 4738, 4729, 4748, 4749, 4758, 4769, 4757, 4767, 4777, 4787, 4797, 4756, 4765, 4774, 4783, 4792, 4746, 4745, 4744, 4743, 4742, 4741, 4740, 4736, 4725, 4714, 4703, 4838, 4828, 4818, 4808, 4839, 4849, 4859, 4858, 4868, 4878, 4888, 4898, 4857, 4866, 4875, 4884, 4893, 4847, 4846, 4845, 4844, 4843, 4842, 4841, 4840, 4837, 4826, 4815, 4804, 4939, 4929, 4919, 4909, 4959, 4969, 4979, 4989, 4999, 4958, 4967, 4976, 4985, 4994, 4948, 4947, 4946, 4945, 4944, 4943, 4942, 4941, 4940, 4938, 4927, 4916, 4905, 5040, 5030, 5020, 5010, 5000, 5041, 5032, 5023, 5014, 5005, 5051, 5052, 5053, 5054, 5055, 5056, 5057, 5058, 5059, 5061, 5072, 5083, 5094, 5060, 5070, 5080, 5090, 5141, 5131, 5121, 5111, 5101, 5142, 5133, 5124, 5115, 5106, 5152, 5153, 5154, 5155, 5156, 5157, 5158, 5159, 5162, 5173, 5184, 5195, 5161, 5171, 5181, 5191, 5160, 5150, 5140, 5242, 5232, 5222, 5212, 5202, 5243, 5234, 5225, 5216, 5207, 5253, 5254, 5255, 5256, 5257, 5258, 5259, 5263, 5274, 5285, 5296, 5262, 5272, 5282, 5292, 5261, 5270, 5251, 5250, 5241, 5230, 5343, 5333, 5323, 5313, 5303, 5344, 5335, 5326, 5317, 5308, 5354, 5355, 5356, 5357, 5358, 5359, 5364, 5375, 5386, 5397, 5363, 5373, 5383, 5393, 5362, 5371, 5380, 5352, 5351, 5350, 5342, 5331, 5320, 5444, 5434, 5424, 5414, 5404, 5445, 5436, 5427, 5418, 5409, 5455, 5456, 5457, 5458, 5459, 5465, 5476, 5487, 5498, 5464, 5474, 5484, 5494, 5463, 5472, 5481, 5490, 5453, 5452, 5451, 5450, 5443, 5432, 5421, 5410, 5545, 5535, 5525, 5515, 5505, 5546, 5537, 5528, 5519, 5556, 5557, 5558, 5559, 5566, 5577, 5588, 5599, 5565, 5575, 5585, 5595, 5564, 5573, 5582, 5591, 5554, 5553, 5552, 5551, 5550, 5544, 5533, 5522, 5511, 5500, 5646, 5636, 5626, 5616, 5606, 5647, 5638, 5629, 5657, 5658, 5659, 5667, 5678, 5689, 5666, 5676, 5686, 5696, 5665, 5674, 5683, 5692, 5655, 5654, 5653, 5652, 5651, 5650, 5645, 5634, 5623, 5612, 5601, 5747, 5737, 5727, 5717, 5707, 5748, 5739, 5758, 5759, 5768, 5779, 5767, 5777, 5787, 5797, 5766, 5775, 5784, 5793, 5756, 5755, 5754, 5753, 5752, 5751, 5750, 5746, 5735, 5724, 5713, 5702, 5848, 5838, 5828, 5818, 5808, 5849, 5859, 5869, 5868, 5878, 5888, 5898, 5867, 5876, 5885, 5894, 5857, 5856, 5855, 5854, 5853, 5852, 5851, 5850, 5847, 5836, 5825, 5814, 5803, 5949, 5939, 5929, 5919, 5909, 5969, 5979, 5989, 5999, 5968, 5977, 5986, 5995, 5958, 5957, 5956, 5955, 5954, 5953, 5952, 5951, 5950, 5948, 5937, 5926, 5915, 5904, 6050, 6040, 6030, 6020, 6010, 6000, 6051, 6042, 6033, 6024, 6015, 6006, 6061, 6062, 6063, 6064, 6065, 6066, 6067, 6068, 6069, 6071, 6082, 6093, 6070, 6080, 6090, 6151, 6141, 6131, 6121, 6111, 6101, 6152, 6143, 6134, 6125, 6116, 6107, 6162, 6163, 6164, 6165, 6166, 6167, 6168, 6169, 6172, 6183, 6194, 6171, 6181, 6191, 6170, 6160, 6150, 6252, 6242, 6232, 6222, 6212, 6202, 6253, 6244, 6235, 6226, 6217, 6208, 6263, 6264, 6265, 6266, 6267, 6268, 6269, 6273, 6284, 6295, 6272, 6282, 6292, 6271, 6280, 6261, 6260, 6251, 6240, 6353, 6343, 6333, 6323, 6313, 6303, 6354, 6345, 6336, 6327, 6318, 6309, 6364, 6365, 6366, 6367, 6368, 6369, 6374, 6385, 6396, 6373, 6383, 6393, 6372, 6381, 6390, 6362, 6361, 6360, 6352, 6341, 6330, 6454, 6444, 6434, 6424, 6414, 6404, 6455, 6446, 6437, 6428, 6419, 6465, 6466, 6467, 6468, 6469, 6475, 6486, 6497, 6474, 6484, 6494, 6473, 6482, 6491, 6463, 6462, 6461, 6460, 6453, 6442, 6431, 6420, 6555, 6545, 6535, 6525, 6515, 6505, 6556, 6547, 6538, 6529, 6566, 6567, 6568, 6569, 6576, 6587, 6598, 6575, 6585, 6595, 6574, 6583, 6592, 6564, 6563, 6562, 6561, 6560, 6554, 6543, 6532, 6521, 6510, 6656, 6646, 6636, 6626, 6616, 6606, 6657, 6648, 6639, 6667, 6668, 6669, 6677, 6688, 6699, 6676, 6686, 6696, 6675, 6684, 6693, 6665, 6664, 6663, 6662, 6661, 6660, 6655, 6644, 6633, 6622, 6611, 6600, 6757, 6747, 6737, 6727, 6717, 6707, 6758, 6749, 6768, 6769, 6778, 6789, 6777, 6787, 6797, 6776, 6785, 6794, 6766, 6765, 6764, 6763, 6762, 6761, 6760, 6756, 6745, 6734, 6723, 6712, 6701, 6858, 6848, 6838, 6828, 6818, 6808, 6859, 6869, 6879, 6878, 6888, 6898, 6877, 6886, 6895, 6867, 6866, 6865, 6864, 6863, 6862, 6861, 6860, 6857, 6846, 6835, 6824, 6813, 6802, 6959, 6949, 6939, 6929, 6919, 6909, 6979, 6989, 6999, 6978, 6987, 6996, 6968, 6967, 6966, 6965, 6964, 6963, 6962, 6961, 6960, 6958, 6947, 6936, 6925, 6914, 6903, 7060, 7050, 7040, 7030, 7020, 7010, 7000, 7061, 7052, 7043, 7034, 7025, 7016, 7007, 7071, 7072, 7073, 7074, 7075, 7076, 7077, 7078, 7079, 7081, 7092, 7080, 7090, 7161, 7151, 7141, 7131, 7121, 7111, 7101, 7162, 7153, 7144, 7135, 7126, 7117, 7108, 7172, 7173, 7174, 7175, 7176, 7177, 7178, 7179, 7182, 7193, 7181, 7191, 7180, 7170, 7160, 7262, 7252, 7242, 7232, 7222, 7212, 7202, 7263, 7254, 7245, 7236, 7227, 7218, 7209, 7273, 7274, 7275, 7276, 7277, 7278, 7279, 7283, 7294, 7282, 7292, 7281, 7290, 7271, 7270, 7261, 7250, 7363, 7353, 7343, 7333, 7323, 7313, 7303, 7364, 7355, 7346, 7337, 7328, 7319, 7374, 7375, 7376, 7377, 7378, 7379, 7384, 7395, 7383, 7393, 7382, 7391, 7372, 7371, 7370, 7362, 7351, 7340, 7464, 7454, 7444, 7434, 7424, 7414, 7404, 7465, 7456, 7447, 7438, 7429, 7475, 7476, 7477, 7478, 7479, 7485, 7496, 7484, 7494, 7483, 7492, 7473, 7472, 7471, 7470, 7463, 7452, 7441, 7430, 7565, 7555, 7545, 7535, 7525, 7515, 7505, 7566, 7557, 7548, 7539, 7576, 7577, 7578, 7579, 7586, 7597, 7585, 7595, 7584, 7593, 7574, 7573, 7572, 7571, 7570, 7564, 7553, 7542, 7531, 7520, 7666, 7656, 7646, 7636, 7626, 7616, 7606, 7667, 7658, 7649, 7677, 7678, 7679, 7687, 7698, 7686, 7696, 7685, 7694, 7675, 7674, 7673, 7672, 7671, 7670, 7665, 7654, 7643, 7632, 7621, 7610, 7767, 7757, 7747, 7737, 7727, 7717, 7707, 7768, 7759, 7778, 7779, 7788, 7799, 7787, 7797, 7786, 7795, 7776, 7775, 7774, 7773, 7772, 7771, 7770, 7766, 7755, 7744, 7733, 7722, 7711, 7700, 7868, 7858, 7848, 7838, 7828, 7818, 7808, 7869, 7879, 7889, 7888, 7898, 7887, 7896, 7877, 7876, 7875, 7874, 7873, 7872, 7871, 7870, 7867, 7856, 7845, 7834, 7823, 7812, 7801, 7969, 7959, 7949, 7939, 7929, 7919, 7909, 7989, 7999, 7988, 7997, 7978, 7977, 7976, 7975, 7974, 7973, 7972, 7971, 7970, 7968, 7957, 7946, 7935, 7924, 7913, 7902, 8070, 8060, 8050, 8040, 8030, 8020, 8010, 8000, 8071, 8062, 8053, 8044, 8035, 8026, 8017, 8008, 8081, 8082, 8083, 8084, 8085, 8086, 8087, 8088, 8089, 8091, 8090, 8171, 8161, 8151, 8141, 8131, 8121, 8111, 8101, 8172, 8163, 8154, 8145, 8136, 8127, 8118, 8109, 8182, 8183, 8184, 8185, 8186, 8187, 8188, 8189, 8192, 8191, 8190, 8180, 8170, 8272, 8262, 8252, 8242, 8232, 8222, 8212, 8202, 8273, 8264, 8255, 8246, 8237, 8228, 8219, 8283, 8284, 8285, 8286, 8287, 8288, 8289, 8293, 8292, 8291, 8281, 8280, 8271, 8260, 8373, 8363, 8353, 8343, 8333, 8323, 8313, 8303, 8374, 8365, 8356, 8347, 8338, 8329, 8384, 8385, 8386, 8387, 8388, 8389, 8394, 8393, 8392, 8382, 8381, 8380, 8372, 8361, 8350, 8474, 8464, 8454, 8444, 8434, 8424, 8414, 8404, 8475, 8466, 8457, 8448, 8439, 8485, 8486, 8487, 8488, 8489, 8495, 8494, 8493, 8483, 8482, 8481, 8480, 8473, 8462, 8451, 8440, 8575, 8565, 8555, 8545, 8535, 8525, 8515, 8505, 8576, 8567, 8558, 8549, 8586, 8587, 8588, 8589, 8596, 8595, 8594, 8584, 8583, 8582, 8581, 8580, 8574, 8563, 8552, 8541, 8530, 8676, 8666, 8656, 8646, 8636, 8626, 8616, 8606, 8677, 8668, 8659, 8687, 8688, 8689, 8697, 8696, 8695, 8685, 8684, 8683, 8682, 8681, 8680, 8675, 8664, 8653, 8642, 8631, 8620, 8777, 8767, 8757, 8747, 8737, 8727, 8717, 8707, 8778, 8769, 8788, 8789, 8798, 8797, 8796, 8786, 8785, 8784, 8783, 8782, 8781, 8780, 8776, 8765, 8754, 8743, 8732, 8721, 8710, 8878, 8868, 8858, 8848, 8838, 8828, 8818, 8808, 8879, 8889, 8899, 8898, 8897, 8887, 8886, 8885, 8884, 8883, 8882, 8881, 8880, 8877, 8866, 8855, 8844, 8833, 8822, 8811, 8800, 8979, 8969, 8959, 8949, 8939, 8929, 8919, 8909, 8999, 8998, 8988, 8987, 8986, 8985, 8984, 8983, 8982, 8981, 8980, 8978, 8967, 8956, 8945, 8934, 8923, 8912, 8901, 9080, 9070, 9060, 9050, 9040, 9030, 9020, 9010, 9000, 9081, 9072, 9063, 9054, 9045, 9036, 9027, 9018, 9009, 9091, 9092, 9093, 9094, 9095, 9096, 9097, 9098, 9099, 9181, 9171, 9161, 9151, 9141, 9131, 9121, 9111, 9101, 9182, 9173, 9164, 9155, 9146, 9137, 9128, 9119, 9192, 9193, 9194, 9195, 9196, 9197, 9198, 9199, 9190, 9180, 9282, 9272, 9262, 9252, 9242, 9232, 9222, 9212, 9202, 9283, 9274, 9265, 9256, 9247, 9238, 9229, 9293, 9294, 9295, 9296, 9297, 9298, 9299, 9291, 9290, 9281, 9270, 9383, 9373, 9363, 9353, 9343, 9333, 9323, 9313, 9303, 9384, 9375, 9366, 9357, 9348, 9339, 9394, 9395, 9396, 9397, 9398, 9399, 9392, 9391, 9390, 9382, 9371, 9360, 9484, 9474, 9464, 9454, 9444, 9434, 9424, 9414, 9404, 9485, 9476, 9467, 9458, 9449, 9495, 9496, 9497, 9498, 9499, 9493, 9492, 9491, 9490, 9483, 9472, 9461, 9450, 9585, 9575, 9565, 9555, 9545, 9535, 9525, 9515, 9505, 9586, 9577, 9568, 9559, 9596, 9597, 9598, 9599, 9594, 9593, 9592, 9591, 9590, 9584, 9573, 9562, 9551, 9540, 9686, 9676, 9666, 9656, 9646, 9636, 9626, 9616, 9606, 9687, 9678, 9669, 9697, 9698, 9699, 9695, 9694, 9693, 9692, 9691, 9690, 9685, 9674, 9663, 9652, 9641, 9630, 9787, 9777, 9767, 9757, 9747, 9737, 9727, 9717, 9707, 9788, 9779, 9798, 9799, 9796, 9795, 9794, 9793, 9792, 9791, 9790, 9786, 9775, 9764, 9753, 9742, 9731, 9720, 9888, 9878, 9868, 9858, 9848, 9838, 9828, 9818, 9808, 9889, 9899, 9897, 9896, 9895, 9894, 9893, 9892, 9891, 9890, 9887, 9876, 9865, 9854, 9843, 9832, 9821, 9810, 9989, 9979, 9969, 9959, 9949, 9939, 9929, 9919, 9909, 9998, 9997, 9996, 9995, 9994, 9993, 9992, 9991, 9990, 9988, 9977, 9966, 9955, 9944, 9933, 9922, 9911, 9900, 21, 12, 122, 120, 113, 223, 221, 214, 210, 324, 322, 315, 311, 425, 423, 416, 412, 526, 524, 517, 513, 627, 625, 618, 614, 728, 726, 719, 715, 829, 827, 816, 928, 917, 1031, 1022, 1002, 1132, 1130, 1123, 1103, 1233, 1231, 1224, 1220, 1204, 1200, 1334, 1332, 1325, 1321, 1305, 1301, 1435, 1433, 1426, 1422, 1406, 1402, 1536, 1534, 1527, 1523, 1507, 1503, 1637, 1635, 1628, 1624, 1608, 1604, 1738, 1736, 1729, 1725, 1709, 1705, 1839, 1837, 1826, 1806, 1938, 1927, 1907, 2041, 2001, 2032, 2012, 2142, 2140, 2102, 2100, 2133, 2113, 2243, 2241, 2203, 2201, 2234, 2230, 2214, 2210, 2344, 2342, 2304, 2302, 2335, 2331, 2315, 2311, 2445, 2443, 2405, 2403, 2436, 2432, 2416, 2412, 2546, 2544, 2506, 2504, 2537, 2533, 2517, 2513, 2647, 2645, 2607, 2605, 2638, 2634, 2618, 2614, 2748, 2746, 2708, 2706, 2739, 2735, 2719, 2715, 2849, 2847, 2809, 2807, 2836, 2816, 2948, 2908, 2937, 2917, 3051, 3011, 3042, 3022, 3152, 3150, 3112, 3110, 3143, 3123, 3253, 3251, 3213, 3211, 3244, 3240, 3224, 3220, 3354, 3352, 3314, 3312, 3345, 3341, 3325, 3321, 3455, 3453, 3415, 3413, 3446, 3442, 3426, 3422, 3556, 3554, 3516, 3514, 3547, 3543, 3527, 3523, 3657, 3655, 3617, 3615, 3648, 3644, 3628, 3624, 3758, 3756, 3718, 3716, 3749, 3745, 3729, 3725, 3859, 3857, 3819, 3817, 3846, 3826, 3958, 3918, 3947, 3927, 4061, 4021, 4052, 4032, 4162, 4160, 4122, 4120, 4153, 4133, 4263, 4261, 4223, 4221, 4254, 4250, 4234, 4230, 4364, 4362, 4324, 4322, 4355, 4351, 4335, 4331, 4465, 4463, 4425, 4423, 4456, 4452, 4436, 4432, 4566, 4564, 4526, 4524, 4557, 4553, 4537, 4533, 4667, 4665, 4627, 4625, 4658, 4654, 4638, 4634, 4768, 4766, 4728, 4726, 4759, 4755, 4739, 4735, 4869, 4867, 4829, 4827, 4856, 4836, 4968, 4928, 4957, 4937, 5071, 5031, 5062, 5042, 5172, 5170, 5132, 5130, 5163, 5143, 5273, 5271, 5233, 5231, 5264, 5260, 5244, 5240, 5374, 5372, 5334, 5332, 5365, 5361, 5345, 5341, 5475, 5473, 5435, 5433, 5466, 5462, 5446, 5442, 5576, 5574, 5536, 5534, 5567, 5563, 5547, 5543, 5677, 5675, 5637, 5635, 5668, 5664, 5648, 5644, 5778, 5776, 5738, 5736, 5769, 5765, 5749, 5745, 5879, 5877, 5839, 5837, 5866, 5846, 5978, 5938, 5967, 5947, 6081, 6041, 6072, 6052, 6182, 6180, 6142, 6140, 6173, 6153, 6283, 6281, 6243, 6241, 6274, 6270, 6254, 6250, 6384, 6382, 6344, 6342, 6375, 6371, 6355, 6351, 6485, 6483, 6445, 6443, 6476, 6472, 6456, 6452, 6586, 6584, 6546, 6544, 6577, 6573, 6557, 6553, 6687, 6685, 6647, 6645, 6678, 6674, 6658, 6654, 6788, 6786, 6748, 6746, 6779, 6775, 6759, 6755, 6889, 6887, 6849, 6847, 6876, 6856, 6988, 6948, 6977, 6957, 7091, 7051, 7082, 7062, 7192, 7190, 7152, 7150, 7183, 7163, 7293, 7291, 7253, 7251, 7284, 7280, 7264, 7260, 7394, 7392, 7354, 7352, 7385, 7381, 7365, 7361, 7495, 7493, 7455, 7453, 7486, 7482, 7466, 7462, 7596, 7594, 7556, 7554, 7587, 7583, 7567, 7563, 7697, 7695, 7657, 7655, 7688, 7684, 7668, 7664, 7798, 7796, 7758, 7756, 7789, 7785, 7769, 7765, 7899, 7897, 7859, 7857, 7886, 7866, 7998, 7958, 7987, 7967, 8061, 8092, 8072, 8162, 8160, 8193, 8173, 8263, 8261, 8294, 8290, 8274, 8270, 8364, 8362, 8395, 8391, 8375, 8371, 8465, 8463, 8496, 8492, 8476, 8472, 8566, 8564, 8597, 8593, 8577, 8573, 8667, 8665, 8698, 8694, 8678, 8674, 8768, 8766, 8799, 8795, 8779, 8775, 8869, 8867, 8896, 8876, 8968, 8997, 8977, 9071, 9082, 9172, 9170, 9183, 9273, 9271, 9284, 9280, 9374, 9372, 9385, 9381, 9475, 9473, 9486, 9482, 9576, 9574, 9587, 9583, 9677, 9675, 9688, 9684, 9778, 9776, 9789, 9785, 9879, 9877, 9886, 9978, 9987 };

std::vector<int> Board::legalMoves()
{
	std::vector<int> moves;
	Pieces* pieces;
	std::unordered_set<int> guardZone;
	if (turn == WHITE)
	{
		pieces = &whitePieces;
		for (int i = 0; i < 2; i++) 
		{
			if (!blackPieces.guards[i].isCaptured())
			{
				std::vector<int> zone = blackPieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	else
	{
		pieces = &blackPieces;
	}
	std::array<int, 2> fromPos;

	//Khan's move
	fromPos = pieces->khan.getPosition();
	for (int i = 0; i < 8; i++)
	{
		int newRow = fromPos[0] + QUEENMOVEROW[i];
		int newCol = fromPos[1] + QUEENMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if (colorOfSquare(newRow, newCol) != turn)
		{
			int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
			if (isChecked(num))
			{
				moves.emplace_back(num);
			}
		}
	}

	//Lion's move
	for (int i = 0; i < 11; i++)
	{
		if (i == 0 && !pieces->lion.isCaptured())
		{
			fromPos = pieces->lion.getPosition();
		}
		else if (i != 0)
		{
			if (pieces->hounds[i - 1].isPromoted() && !pieces->hounds[i - 1].isCaptured())
			{
				fromPos = pieces->hounds[i - 1].getPosition();
			}
			else
			{
				continue;
			}
		}
		else
		{
			continue;
		}
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Guards' move
	for (auto& guard : pieces->guards)
	{
		if (guard.isCaptured())
		{
			continue;
		}
		fromPos = guard.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			for (int k = 1; k < 3; k++)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (board[newRow][newCol] == EMPTY)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						moves.emplace_back(num);
					}
				}
				else if (colorOfSquare(newRow, newCol) != turn)
				{
					if (j % 2 == 1 && k == 1)
					{
						int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
						if (isChecked(num))
						{
							moves.emplace_back(num);
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Camels' move
	for (auto& camel : pieces->camels)
	{
		if (camel.isCaptured())
		{
			continue;
		}
		fromPos = camel.getPosition();
		for (int j = 1; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Terges' move
	for (auto& terge : pieces->terges)
	{
		if (terge.isCaptured())
		{
			continue;
		}
		fromPos = terge.getPosition();
		for (int j = 0; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Horses' move
	for (auto& horse : pieces->horses)
	{
		if (horse.isCaptured())
		{
			continue;
		}
		fromPos = horse.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			newRow += HORSEMOVEROW[j];
			newCol += HORSEMOVECOL[j];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (board[newRow][newCol] != turn)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
		}
	}

	//Hounds' move
	int houndDiection;
	if (turn == Color::WHITE)
	{
		houndDiection = -1;
	}
	else
	{
		houndDiection = 1;
	}
	for (auto& hound : pieces->hounds)
	{
		if (hound.isCaptured() || hound.isPromoted())
		{
			continue;
		}
		fromPos = hound.getPosition();
		int moveDistance = 1;
		if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
		{
			moveDistance = 3;
		}
		for (int i = 1; i <= moveDistance; i++)
		{
			int newRow = fromPos[0] + houndDiection * i;
			if (newRow < 0 || newRow > 9)
			{
				continue;
			}
			if (board[newRow][fromPos[1]] == EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, fromPos[1]});
				if (isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
			else
			{
				continue;
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + fromPos[1]) != guardZone.end())
			{
				break;
			}
		}
		int newRow = fromPos[0] + houndDiection;
		for (int i = -1; i <= 1; i += 2)
		{
			int newCol = fromPos[1] + i;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn && board[newRow][newCol] != EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
		}
	}
	return moves;
}
bool Board::hasLegalMoves()
{
	Pieces* pieces;
	std::unordered_set<int> guardZone;
	if (turn == WHITE)
	{
		pieces = &whitePieces;
		for (int i = 0; i < 2; i++)
		{
			if (!blackPieces.guards[i].isCaptured())
			{
				std::vector<int> zone = blackPieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	else
	{
		pieces = &blackPieces;
	}
	std::array<int, 2> fromPos;

	//Khan's move
	fromPos = pieces->khan.getPosition();
	for (int i = 0; i < 8; i++)
	{
		int newRow = fromPos[0] + QUEENMOVEROW[i];
		int newCol = fromPos[1] + QUEENMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if (colorOfSquare(newRow, newCol) != turn)
		{
			int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
			if (isChecked(num))
			{
				return true;
			}
		}
	}

	//Lion's move
	for (int i = 0; i < 11; i++)
	{
		if (i == 0 && !pieces->lion.isCaptured())
		{
			fromPos = pieces->lion.getPosition();
		}
		else if (i != 0)
		{
			if (pieces->hounds[i - 1].isPromoted() && !pieces->hounds[i - 1].isCaptured())
			{
				fromPos = pieces->hounds[i - 1].getPosition();
			}
			else
			{
				continue;
			}
		}
		else
		{
			continue;
		}
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Guards' move
	for (auto& guard : pieces->guards)
	{
		if (guard.isCaptured())
		{
			continue;
		}
		fromPos = guard.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			for (int k = 1; k < 3; k++)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (board[newRow][newCol] == EMPTY)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						return true;
					}
				}
				else if (colorOfSquare(newRow, newCol) != turn)
				{
					if (j % 2 == 1 && k == 1)
					{
						int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
						if (isChecked(num))
						{
							return true;
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Camels' move
	for (auto& camel : pieces->camels)
	{
		if (camel.isCaptured())
		{
			continue;
		}
		fromPos = camel.getPosition();
		for (int j = 1; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Terges' move
	for (auto& terge : pieces->terges)
	{
		if (terge.isCaptured())
		{
			continue;
		}
		fromPos = terge.getPosition();
		for (int j = 0; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (isChecked(num))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Horses' move
	for (auto& horse : pieces->horses)
	{
		if (horse.isCaptured())
		{
			continue;
		}
		fromPos = horse.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			newRow += HORSEMOVEROW[j];
			newCol += HORSEMOVECOL[j];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (board[newRow][newCol] != turn)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (isChecked(num))
				{
					return true;
				}
			}
		}
	}

	//Hounds' move
	int houndDiection;
	if (turn == Color::WHITE)
	{
		houndDiection = -1;
	}
	else
	{
		houndDiection = 1;
	}
	for (auto& hound : pieces->hounds)
	{
		if (hound.isCaptured() || hound.isPromoted())
		{
			continue;
		}
		fromPos = hound.getPosition();
		int moveDistance = 1;
		if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
		{
			moveDistance = 3;
		}
		for (int i = 1; i <= moveDistance; i++)
		{
			int newRow = fromPos[0] + houndDiection * i;
			if (newRow < 0 || newRow > 9)
			{
				continue;
			}
			if (board[newRow][fromPos[1]] == EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, fromPos[1]});
				if (isChecked(num))
				{
					return true;
				}
			}
			else
			{
				continue;
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + fromPos[1]) != guardZone.end())
			{
				break;
			}
		}
		int newRow = fromPos[0] + houndDiection;
		for (int i = -1; i <= 1; i += 2)
		{
			int newCol = fromPos[1] + i;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn && board[newRow][newCol] != EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (isChecked(num))
				{
					return true;
				}
			}
		}
	}
	return false;
}

bool Board::isChecked(int move)
{
	std::array<std::array<Square, 10>, 10> newBoard = board;
	if(move > 0)
	{
		std::array<int, 4> moveArray = NumToMove(move);
		newBoard[moveArray[2]][moveArray[3]] = newBoard[moveArray[0]][moveArray[1]];
		newBoard[moveArray[0]][moveArray[1]] = EMPTY;
	}

	//Find the guard Zone
	std::unordered_set<int> guardZone;
	for (int i = 0; i < 10; i++)
	{
		for (int j = 0; j < 10; j++)
		{
			if ((newBoard[i][j] == WHITEGUARD && turn == WHITE)
				|| (newBoard[i][j] == BLACKGUARD && turn == BLACK))
			{
				std::vector<int> zone = Guard(i, j, turn).getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}

	//Find the Khan
	std::array<int, 2> khanPos{ -1, -1 };
	for (int i = 0; i < 10; i++)
	{
		if (khanPos[0] != -1)
		{
			break;
		}
		for (int j = 0; j < 10; j++)
		{
			if ((newBoard[i][j] == WHITEKHAN && turn == WHITE)
				|| (newBoard[i][j] == BLACKKHAN && turn == BLACK))
			{
				khanPos[0] = i;
				khanPos[1] = j;
				break;
			}
		}
	}

	//Check Khan is being chekced or not
	if (turn == WHITE)
	{
		int newRow = khanPos[0] - 1;
		int newCol = khanPos[1] - 1;
		if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == BLACKHOUND)
			{
				return true;
			}
		}
		newCol += 2;
		if(newCol >=0 && newCol <=9)
		{
			if (newBoard[newRow][newCol] == BLACKHOUND)
			{
				return true;
			}
		}
	}
	else
	{
		int newRow = khanPos[0] + 1;
		int newCol = khanPos[1] - 1;
		if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == WHITEHOUND)
			{
				return true;
			}
		}
		newCol += 2;
		if (newCol >= 0 && newCol <= 9)
		{
			if (newBoard[newRow][newCol] == WHITEHOUND)
			{
				return true;
			}
		}
	}
	for (int i = 0; i < 8; i++)
	{
		int newRow = khanPos[0] + HORSEMOVEROW[i];
		int newCol = khanPos[1] + HORSEMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if ((newBoard[newRow][newCol] == WHITEHORSE && turn == BLACK)
			|| (newBoard[newRow][newCol] == BLACKHORSE && turn == WHITE))
		{
			return true;
		}
	}
	for (int i = 0; i < 8; i++)
	{
		int newRow = khanPos[0];
		int newCol = khanPos[1];
		int distance = 0;

		while (true) 
		{
			newRow += QUEENMOVEROW[i];
			newCol += QUEENMOVECOL[i];
			distance++;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				break;
			}
			if ((newBoard[newRow][newCol] == WHITELION && turn == BLACK)
				|| (newBoard[newRow][newCol] == BLACKLION && turn == WHITE))
			{
				return true;
			}
			if (distance == 1)
			{
				if ((newBoard[newRow][newCol] == WHITEKHAN && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKKHAN && turn == WHITE))
				{
					return true;
				}
				if (i % 2 == 1)
				{
					if ((newBoard[newRow][newCol] == WHITEGUARD && turn == BLACK)
						|| (newBoard[newRow][newCol] == BLACKGUARD && turn == WHITE))
					{
						return true;
					}
				}
			}
			if (i % 2 == 0)
			{
				if ((newBoard[newRow][newCol] == WHITETERGE && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKTERGE && turn == WHITE))
				{
					return true;
				}
			}
			else
			{
				if ((newBoard[newRow][newCol] == WHITECAMEL && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKCAMEL && turn == WHITE))
				{
					return true;
				}
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
			{
				break;
			}
		}
	}
	return false;
}
Color Board::colorOfSquare(int row, int col)
{
	if (board[row][col] == EMPTY)
	{
		return DRAW;
	}
	switch (board[row][col])
	{
		case WHITEKHAN:
		case WHITELION:
		case WHITEGUARD:
		case WHITECAMEL:
		case WHITEHORSE:
		case WHITETERGE:
		case WHITEHOUND:
			return WHITE;
		default:
			return BLACK;
	}
}
int Board::repeatCount()
{
	int repeats = 0;
	for (int i = preBoards.size(); i >= 0; i--)
	{
		if (i % 2 == 1)
		{
			continue;
		}
		if (preBoards[i] == board)
		{
			repeats++;
		}
	}
	return repeats;
}

Board::Board()
{
	for (int i = 2; i < 8; i++)
	{
		board[i].fill(EMPTY);
	}
	board[0][0] = board[0][9] = BLACKTERGE;
	board[0][1] = board[0][8] = BLACKHORSE;
	board[0][2] = board[0][7] = BLACKCAMEL;
	board[0][3] = board[0][6] = BLACKGUARD;
	board[0][4] = BLACKLION;
	board[0][5] = BLACKKHAN;

	board[9][0] = board[9][9] = WHITETERGE;
	board[9][1] = board[9][8] = WHITEHORSE;
	board[9][2] = board[9][7] = WHITECAMEL;
	board[9][3] = board[9][6] = WHITEGUARD;
	board[9][5] = WHITELION;
	board[9][4] = WHITEKHAN;

	for (int i = 0; i < 10; i++)
	{
		board[1][i] = BLACKHOUND;
		board[8][i] = WHITEHOUND;
	}

	turn = WHITE;
	noProcess = 0;
}