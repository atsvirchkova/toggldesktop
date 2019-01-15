#ifndef AUTOCOMPLETECOMBOBOX_H
#define AUTOCOMPLETECOMBOBOX_H

#include <QComboBox>
#include <QLineEdit>
#include <QCompleter>
#include <QSortFilterProxyModel>

class AutocompleteLineEdit;
class AutocompleteCompleter;
class AutocompleteProxyModel;
class AutocompleteListView;

class AutocompleteComboBox : public QComboBox {
    friend class AutocompleteLineEdit;
    Q_OBJECT
public:
    AutocompleteComboBox(QWidget *parent = nullptr);

    void setModel(QAbstractItemModel *model);

    bool eventFilter(QObject *o, QEvent *e) override;

protected:
    void keyPressEvent(QKeyEvent *event) override;

private slots:
    void onModelChanged();
    void onDropdownVisibleChanged();

private:
    AutocompleteLineEdit *lineEdit;
    AutocompleteCompleter *completer;
    AutocompleteProxyModel *proxyModel;
    AutocompleteListView *listView;

    QString oldLabel {};
};

class AutocompleteLineEdit : public QLineEdit {
    Q_OBJECT
    friend class AutocompleteComboBox;
public:
    AutocompleteLineEdit(AutocompleteComboBox *parent = nullptr);

    AutocompleteComboBox *comboBox();

protected:
    void keyPressEvent(QKeyEvent *event) override;
};

class AutocompleteCompleter : public QCompleter {
    Q_OBJECT
    friend class AutocompleteComboBox;
public:
    AutocompleteCompleter(QWidget *parent = nullptr);

    bool eventFilter(QObject *o, QEvent *e) override;
};

class AutocompleteProxyModel : public QSortFilterProxyModel {
    Q_OBJECT
    friend class AutocompleteComboBox;
public:
    AutocompleteProxyModel(QObject *parent = nullptr);

    bool filterAcceptsRow(int source_row, const QModelIndex &source_parent) const override;
};

#endif // AUTOCOMPLETECOMBOBOX_H
