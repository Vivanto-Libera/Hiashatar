#include "Board.h"

PYBIND11_MODULE(Hiashatar, m)
{
	py::enum_<Square>(m, "Square")
		.value("EMPTY", Square::EMPTY)
		.value("WHITEKHAN", Square::WHITEKHAN)
		.value("WHITELION", Square::WHITELION)
		.value("WHITEBODYGUARD", Square::WHITEGUARD)
		.value("WHITECAMEL", Square::WHITECAMEL)
		.value("WHITEHORSE", Square::WHITEHORSE)
		.value("WHITETERGE", Square::WHITETERGE)
		.value("WHITEHOUND", Square::WHITEHOUND)
		.value("BLACKKHAN", Square::BLACKKHAN)
		.value("BLACKLION", Square::BLACKLION)
		.value("BLACKBODYGUARD", Square::BLACKGUARD)
		.value("BLACKCAMEL", Square::BLACKCAMEL)
		.value("BLACKHORSE", Square::BLACKHORSE)
		.value("BLACKTERGE", Square::BLACKTERGE)
		.value("BLACKHOUND", Square::BLACKHOUND)
		.export_values();

	py::enum_<Color>(m, "Color")
		.value("BLACK", Color::BLACK)
		.value("WHITE", Color::WHITE)
		.value("DRAW", Color::DRAW)
		.value("NOTEND", Color::NOTEND)
		.export_values();

		
};