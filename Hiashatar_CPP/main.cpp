#include "Board.h"

PYBIND11_MODULE(Hiashatar, m)
{
	py::enum_<Square>(m, "Square")
		.value("EMPTY", Square::EMPTY)
		.value("WHITEKHAN", Square::WHITEKHAN)
		.value("WHITELION", Square::WHITELION)
		.value("WHITEGUARD", Square::WHITEGUARD)
		.value("WHITECAMEL", Square::WHITECAMEL)
		.value("WHITEHORSE", Square::WHITEHORSE)
		.value("WHITETERGE", Square::WHITETERGE)
		.value("WHITEHOUND", Square::WHITEHOUND)
		.value("BLACKKHAN", Square::BLACKKHAN)
		.value("BLACKLION", Square::BLACKLION)
		.value("BLACKGUARD", Square::BLACKGUARD)
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

	py::class_<Board>(m, "Board")
		.def(py::init<>())
		.def(py::init<const Board&>())
		.def("isTerminal", &Board::isTerminal)
		.def("legalMoves", &Board::legalMoves)
		.def("neuralworkInput", &Board::neuralworkInput)
		.def("applyMove", &Board::applyMove)
		.def_static("indexToMove", &Board::indexTransToMove)
		.def_static("moveToIndex", &Board::moveToIndex)
		.def_readwrite("board", &Board::board)
		.def_readwrite("turn", &Board::turn);
};